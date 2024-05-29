
import { useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';

import { Formik, Form } from 'formik';

import { Box, Button, Container, Grid, TextField, InputLabel, Select, MenuItem } from '@mui/material';


import LogoImage from '../assets/Logo_TheStreets.png';

// const LogoImage = '../../public/Logo_TheStreets.png';

import { CommunityContext } from '../contexts/CommunityContextProvider';
import { postAndPutReqFunction } from '../utilities/ApiUtilities';
import { loginSchema } from '../utilities/YupSchemas';
import '../styles/LoginStyles.css';
import { useEffect } from 'react';

const apiForLogin = 'https://proj.ruppin.ac.il/cgroup62/test2/tar1/api/Users/Login';

export default function Login() {

    const navigate = useNavigate();
    const { communities } = useContext(CommunityContext);
    const [user, setUser] = useState(JSON.parse(localStorage.getItem('user')) || null);
    // const [errorMessage, setErrorMessage] = useState('');

    useEffect(() => {
        if (user !== null) {
            navigate('/Home');
        }
    }, [])

    async function handlePostLogin(loginDetails) {
        try {
            const loggedInUser = await postAndPutReqFunction(loginDetails, apiForLogin, 'POST');
            localStorage.setItem('user', JSON.stringify(loggedInUser));
            setUser(loggedInUser);
            navigate('/Home');
        } catch (error) {
            console.error('Login error:', error);
            setErrorMessage(error.message);
        }
    }

    const handleFormSubmit = (values, actions) => {

        let userToLogin = {
            communityID: values.community,
            phoneNum: values.phoneNumber,
            password: values.password
        }
        localStorage.setItem('userCommunityId', JSON.stringify(values.community));
        handlePostLogin(userToLogin);
        actions.resetForm();
    };

    const getToMapCommunityPage = () => {
        navigate("/CommunitiesMap");
    }



    return (
        <Container maxWidth="lg" style={{ height: '90vh', width: '75vw', display: 'flex', flexDirection: 'column' }}>
            <Box sx={{ backgroundColor: '#ffffff', display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center', height: '20vh', width: '100%', marginBottom: '2%' }}>
                <img src={LogoImage} alt="Logo"
                    style={{ maxWidth: '80%', }}
                />
            </Box>

            <Box sx={{ backgroundColor: '#ffffff', height: '70vh', width: '100%' }}>
                <Formik
                    initialValues={{ community: '', phoneNumber: '', password: '' }}
                    validationSchema={loginSchema}
                    onSubmit={(values, actions) => {
                        handleFormSubmit(values, actions);
                    }}
                >
                    {formikProps => (
                        <Form style={{ height: '100%', display: 'flex', flexDirection: 'column' }}>
                            <Grid container rowSpacing={4} justifyContent='center'>

                                <Grid item xs={12} sm={10} md={7} lg={7} >
                                    <Box>
                                        <InputLabel id="demo-simple-select-standard-label">:קהילה</InputLabel>
                                        <Select
                                            variant='standard'
                                            className='selectPurple'
                                            onChange={e => formikProps.setFieldValue("community", e.target.value)}
                                            value={formikProps.values.community}
                                            onBlur={formikProps.handleBlur('community')}
                                            error={Boolean(formikProps.errors.community)}
                                        >
                                            <MenuItem value="">
                                                <em>יש לבחור מהרשימה</em>
                                            </MenuItem>

                                            {
                                                communities.map((community) => (
                                                    <MenuItem key={community.communityId} value={community.communityId}>
                                                        {community.name}
                                                    </MenuItem>
                                                ))
                                            }

                                        </Select>

                                        {formikProps.touched.community && formikProps.errors.community && (
                                            <Box className="errorMessage">
                                                {formikProps.errors.community}
                                            </Box>
                                        )}
                                    </Box>
                                </Grid>

                                <Grid item xs={12} >
                                    <Box>
                                        <TextField
                                            id="phoneNumber" label="טלפון נייד:"
                                            type='tel'
                                            variant='standard'
                                            className="inpPurple"
                                            onChange={e => {
                                                formikProps.setFieldValue('phoneNumber', e.target.value);
                                            }}
                                            value={formikProps.values.phoneNumber}
                                            onBlur={formikProps.handleBlur('phoneNumber')}
                                            error={Boolean(formikProps.errors.phoneNumber)}
                                            inputProps={{ maxLength: 10 }}
                                        />

                                        {formikProps.touched.phoneNumber && formikProps.errors.phoneNumber && (
                                            <Box className="errorMessage">
                                                {formikProps.errors.phoneNumber}
                                            </Box>
                                        )}
                                    </Box>
                                </Grid>

                                <Grid item xs={12} >
                                    <Box>
                                        <TextField
                                            id="password"
                                            label="סיסמא:"
                                            type='password'
                                            variant='standard'
                                            className="inpPurple"
                                            onChange={e => {
                                                formikProps.setFieldValue('password', e.target.value);
                                            }}
                                            value={formikProps.values.password}
                                            onBlur={formikProps.handleBlur('password')}
                                            error={Boolean(formikProps.errors.password)}
                                            inputProps={{ maxLength: 10 }}
                                        />

                                        {formikProps.touched.password && formikProps.errors.password && (
                                            <Box className="errorMessage">
                                                {formikProps.errors.password}
                                            </Box>
                                        )}
                                    </Box>
                                </Grid>

                                <Grid item xs={12}>
                                    <Button
                                        id='loginBtn'
                                        type='button'
                                        onClick={formikProps.handleSubmit}
                                    >התחברות</Button>
                                </Grid>


                                <Grid item xs={12}>
                                    <Button variant='contained'
                                        type='button'
                                        id='registerBtn'
                                        onClick={getToMapCommunityPage}
                                    >עדיין לא חבר בשכונה? לחץ כאן</Button>
                                </Grid>
                            </Grid>
                        </Form>
                    )}
                </Formik>
            </Box>
        </Container>
    );
}



