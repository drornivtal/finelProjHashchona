
import { useContext, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import dayjs from 'dayjs';

import { Formik, Form } from 'formik';

import { Box, Button, Container, Grid, TextField, Select, MenuItem, InputLabel } from '@mui/material';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';

import { CommunityContext } from '../contexts/CommunityContextProvider';
import { citiesList, gendersList } from '../utilities/ListsUtilities';
import { registerSchema } from '../utilities/YupSchemas';
import { postAndPutReqFunction } from '../utilities/ApiUtilities';

import LogoImage from '../assets/Logo_TheStreets.png';

import '../styles/RegisterStyles.css';
import '../styles/LoginStyles.css';

export default function Register() {

    const navigate = useNavigate();
    const { state } = useLocation();
    let registerObj = state;

    //for check!
    console.log(registerObj);

    let isManager = registerObj.isManager;

    const { communities } = useContext(CommunityContext);
    // const [user, setUser] = useState(JSON.parse(sessionStorage.getItem('user')) || '');
    const [errorMessage, setErrorMessage] = useState('');

    const apiNewUser = 'https://proj.ruppin.ac.il/cgroup62/test2/tar1/api/Users/InsertNewUser';

    const handleFormSubmit = (values, actions) => {

        let userToRegister = {
            // Add profilePic here!!!
            firstName: values.firstName,
            lastName: values.lastName,
            phoneNum: values.phoneNumber,
            password: values.password,
            gender: values.gender,
            city: values.city,
            street: values.street,
            homeNum: values.homeNumber,
            // birthDate: "1990-05-17T09:56:05.578Z", // fix!
            birthDate: values.birthDate.toISOString(),
            description: values.about,
            isActive: 'f'
        }

        if (!isManager) {
            let newUser = { userToRegister, communityId: registerObj.communityId };

            console.log("this is a regular user: ");
            console.log(newUser);
            // In this point need to post the new user to server!!!
            postAndPutReqFunction(newUser, apiNewUser, 'POST');
            navigate('/');
        }
        else {
            console.log("this is a manager user: ");
            console.log(userToRegister);
            navigate('/NewCommunity', { state: userToRegister });
        }

        actions.resetForm();
    };

    return (
        <Container maxWidth="lg" style={{ height: '90vh', display: 'flex', flexDirection: 'column' }}>

            <Box sx={{ backgroundColor: '#ffffff', display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center', height: '10vh', marginBottom: '10%', marginTop: '3%' }}>
                <img src={LogoImage} alt="Logo" style={{ maxWidth: '45%' }} />
            </Box>

            <Box sx={{ backgroundColor: '#ffffff', height: '80vh' }}>
                <Formik
                    initialValues={{
                        imageUri: '', firstName: '', lastName: '', gender: '',
                        birthDate: dayjs(), // --- today date for defaulte value ---
                        phoneNumber: '', password: '', city: '',
                        street: '', homeNumber: '', about: ''
                    }}
                    validationSchema={registerSchema}
                    onSubmit={(values, actions) => {
                        handleFormSubmit(values, actions);
                    }}
                >
                    {formikProps => (
                        <Form style={{ height: '100%', display: 'flex', flexDirection: 'column' }}>

                            <Grid container rowSpacing={4} justifyContent='center'>

                                <Grid item xs={12} >
                                    <Box>

                                        <TextField
                                            id="imageUri"
                                            label="תמונת פרופיל:"
                                            type='file'
                                            variant='standard'
                                            className={registerObj.inpCssClass}
                                            onChange={e => {
                                                formikProps.setFieldValue('imageUri', e.target.value);
                                            }}
                                            value={formikProps.values.imageUri}
                                            onBlur={formikProps.handleBlur('imageUri')}
                                            error={Boolean(formikProps.errors.imageUri)}
                                        />

                                        {formikProps.touched.imageUri && formikProps.errors.imageUri && (
                                            <Box className="errorMessage">
                                                {formikProps.errors.imageUri}
                                            </Box>
                                        )}
                                    </Box>
                                </Grid>

                                <Grid item xs={12} >
                                    <Box>

                                        <TextField
                                            id="firstName" label="שם פרטי:"
                                            type='text'
                                            variant='standard'
                                            className={registerObj.inpCssClass}
                                            onChange={e => {
                                                formikProps.setFieldValue('firstName', e.target.value);
                                            }}
                                            value={formikProps.values.firstName}
                                            onBlur={formikProps.handleBlur('firstName')}
                                            error={Boolean(formikProps.errors.firstName)}
                                        />

                                        {formikProps.touched.firstName && formikProps.errors.firstName && (
                                            <Box className="errorMessage">
                                                {formikProps.errors.firstName}
                                            </Box>
                                        )}
                                    </Box>
                                </Grid>

                                <Grid item xs={12} >
                                    <Box>
                                        <TextField
                                            id="lastName" label="שם משפחה:"
                                            type='text'
                                            variant='standard'
                                            className={registerObj.inpCssClass}
                                            onChange={e => {
                                                formikProps.setFieldValue('lastName', e.target.value);
                                            }}
                                            value={formikProps.values.lastName}
                                            onBlur={formikProps.handleBlur('lastName')}
                                            error={Boolean(formikProps.errors.lastName)}
                                        />

                                        {formikProps.touched.lastName && formikProps.errors.lastName && (
                                            <Box className="errorMessage">
                                                {formikProps.errors.lastName}
                                            </Box>
                                        )}
                                    </Box>
                                </Grid>

                                <Grid item xs={12} sm={10} md={6} lg={4} >
                                    <Box>
                                        <InputLabel id="labelgenderSelect">:מגדר</InputLabel>
                                        <Select
                                            variant='standard'
                                            className={registerObj.selectCssClass}
                                            onChange={e => formikProps.setFieldValue("gender", e.target.value)}
                                            value={formikProps.values.gender}
                                            onBlur={formikProps.handleBlur('gender')}
                                            error={Boolean(formikProps.errors.gender)}
                                        >
                                            <MenuItem value="">
                                                <em>יש לבחור מהרשימה</em>
                                            </MenuItem>

                                            {
                                                gendersList.map((gender) => (
                                                    <MenuItem key={gender.key} value={gender.finalValue}>
                                                        {gender.value}
                                                    </MenuItem>
                                                ))
                                            }

                                        </Select>

                                        {formikProps.touched.gender && formikProps.errors.gender && (
                                            <Box className="errorMessage">
                                                {formikProps.errors.gender}
                                            </Box>
                                        )}
                                    </Box>
                                </Grid>

                                <Grid item xs={12} >
                                    <Box>
                                        <LocalizationProvider dateAdapter={AdapterDayjs}>
                                            <DatePicker
                                                label="תאריך לידה"
                                                variant='standard'
                                                format='DD/MM/YYYY'
                                                // className='inpYellow'
                                                value={formikProps.values.birthDate}
                                                onChange={userDate => {
                                                    formikProps.setFieldValue('birthDate', userDate);
                                                }}
                                                onBlur={formikProps.handleBlur('birthDate')}
                                                error={Boolean(formikProps.errors.birthDate)}
                                            />
                                        </LocalizationProvider>

                                        {formikProps.touched.birthDate && formikProps.errors.birthDate && (
                                            <Box className="errorMessage">
                                                {formikProps.errors.birthDate}
                                            </Box>
                                        )}
                                    </Box>
                                </Grid>

                                <Grid item xs={12} >
                                    <Box>
                                        <TextField
                                            id="phoneNumber"
                                            label="טלפון נייד:"
                                            type='tel'
                                            variant='standard'
                                            className={registerObj.inpCssClass}
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
                                            className={registerObj.inpCssClass}
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

                                {/* ---Input: city--- */}
                                <Grid item xs={12} sm={10} md={6} lg={4} >
                                    <Box>
                                        <InputLabel id="labelCitySelect">:עיר</InputLabel>
                                        <Select
                                            variant='standard'
                                            className={registerObj.selectCssClass}
                                            onChange={e => formikProps.setFieldValue("city", e.target.value)}
                                            value={formikProps.values.city}
                                            onBlur={formikProps.handleBlur('city')}
                                            error={Boolean(formikProps.errors.city)}
                                        >
                                            <MenuItem value="">
                                                <em>יש לבחור מהרשימה</em>
                                            </MenuItem>
                                            {
                                                citiesList.map((city) => (
                                                    <MenuItem key={city.key} value={city.value}>
                                                        {city.value}
                                                    </MenuItem>
                                                ))
                                            }

                                        </Select>

                                        {formikProps.touched.city && formikProps.errors.city && (
                                            <Box className="errorMessage">
                                                {formikProps.errors.city}
                                            </Box>
                                        )}
                                    </Box>
                                </Grid>

                                {/* ---Input: street--- */}
                                <Grid item xs={12} >
                                    <Box>
                                        <TextField
                                            id="tfRegisterStreet"
                                            label="רחוב:"
                                            type='text'
                                            variant='standard'
                                            className={registerObj.inpCssClass}
                                            onChange={e => {
                                                formikProps.setFieldValue('street', e.target.value);
                                            }}
                                            value={formikProps.values.street}
                                            onBlur={formikProps.handleBlur('street')}
                                            error={Boolean(formikProps.errors.street)}
                                        />

                                        {formikProps.touched.street && formikProps.errors.street && (
                                            <Box className="errorMessage">
                                                {formikProps.errors.street}
                                            </Box>
                                        )}
                                    </Box>
                                </Grid>
                                {/* ---Input: homeNumber--- */}
                                <Grid item xs={12} >
                                    <Box>
                                        <TextField
                                            id="tfRegisterHomeNumber"
                                            label="מספר בית:"
                                            type='text'
                                            variant='standard'
                                            className={registerObj.inpCssClass}
                                            onChange={e => {
                                                formikProps.setFieldValue('homeNumber', e.target.value);
                                            }}
                                            value={formikProps.values.homeNumber}
                                            onBlur={formikProps.handleBlur('homeNumber')}
                                            error={Boolean(formikProps.errors.homeNumber)}
                                        />

                                        {formikProps.touched.homeNumber && formikProps.errors.homeNumber && (
                                            <Box className="errorMessage">
                                                {formikProps.errors.homeNumber}
                                            </Box>
                                        )}
                                    </Box>
                                </Grid>

                                {/* ---Input: about--- */}
                                <Grid item xs={12} >
                                    <Box>
                                        <TextField
                                            multiline
                                            id={registerObj.inpAboutCssId}
                                            label="...קצת על עצמך"
                                            type='text'
                                            variant='standard'
                                            className="inpAboutClass"
                                            onChange={e => {
                                                formikProps.setFieldValue('about', e.target.value);
                                            }}
                                            value={formikProps.values.about}
                                            onBlur={formikProps.handleBlur('about')}
                                            error={Boolean(formikProps.errors.about)}
                                            inputProps={{ maxLength: 100 }}

                                        // sx={{ marginTop: '15%', height: '70px', width: '60%', textAlign: 'center' }}
                                        />

                                        {formikProps.touched.about && formikProps.errors.about && (
                                            <Box className="errorMessage">
                                                {formikProps.errors.about}
                                            </Box>
                                        )}
                                    </Box>
                                </Grid>

                                <Grid item xs={12}>
                                    <Button
                                        id={registerObj.btnCssId}
                                        type='button'
                                        onClick={formikProps.handleSubmit}
                                    > {registerObj.btnText}</Button>
                                </Grid>
                            </Grid>
                        </Form>
                    )}
                </Formik>
            </Box>
        </Container>
    );


}



