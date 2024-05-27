

import { Box, Button, Container, Grid, InputLabel, MenuItem, Select, TextField } from "@mui/material";
import { Form, Formik } from "formik";
import { useLocation, useNavigate } from "react-router-dom";

import { newCommunitySchema } from '../utilities/YupSchemas';

import { citiesList } from "../utilities/ListsUtilities";

import LogoImage from '../assets/Logo_TheStreets.png';
import '../styles/LoginStyles.css';
import '../styles/NewCommunityStyles.css';
import { postAndPutReqFunction } from "../utilities/ApiUtilities";

const apiNewCommunity = 'https://proj.ruppin.ac.il/cgroup62/test2/tar1/api/Communities/InsertNewCommunity';


export default function NewCommunity() {

    const navigate = useNavigate();
    const { state } = useLocation();
    let userManager = state;

    //Check if userobj arrived!
    console.log(userManager);



    const handleFormSubmit = (values, actions) => {

        let community = {
            name: values.communityName,
            city: values.city,
            location: values.street,
            description: values.description,
            primaryPic: "StamImg",
            status: ""
        };

        let newCommunityWithUser = { userManager, community };

        //Check if newCommunityWithUser arrived to this page!
        console.log(newCommunityWithUser);

        postAndPutReqFunction(newCommunityWithUser, apiNewCommunity, 'POST');

        navigate('/');
        actions.resetForm();
    };

    return (
        <Container maxWidth="lg" style={{ height: '90vh', display: 'flex', flexDirection: 'column' }}>

            <Box sx={{ backgroundColor: '#ffffff', display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center', marginBottom: '15%', marginTop: '5%', height: '10vh' }}>
                <img src={LogoImage} alt="Logo" style={{ maxWidth: '50%' }} />
            </Box>

            <Box sx={{ backgroundColor: '#ffffff', height: '80vh' }}>
                <Formik
                    initialValues={{ communityName: '', city: '', street: '', description: '' }}
                    validationSchema={newCommunitySchema}
                    onSubmit={(values, actions) => {
                        console.log('Submitted!');
                        handleFormSubmit(values, actions);
                    }}
                >
                    {formikProps => (
                        <Form style={{ height: '100%', display: 'flex', flexDirection: 'column' }}>
                            <Grid container rowSpacing={4} justifyContent='center'>

                                {/* --- Input: imageUri--- */}
                                <Grid item xs={12} >
                                    <Box>
                                        <TextField
                                            label="תמונת הקהילה:"
                                            type='file'
                                            variant='standard'
                                            className="inpPurple"
                                            onChange={e => {
                                                formikProps.setFieldValue('imageUri', e.target.value);
                                            }}
                                            value={formikProps.values.imageUri}
                                            onBlur={formikProps.handleBlur('imageUri')}
                                            error={Boolean(formikProps.errors.imageUri)}
                                            sx={{ width: '50%' }}
                                        />

                                        {formikProps.touched.imageUri && formikProps.errors.imageUri && (
                                            <Box className="errorMessage">
                                                {formikProps.errors.imageUri}
                                            </Box>
                                        )}
                                    </Box>
                                </Grid>

                                {/* ---Input: city--- */}
                                <Grid item xs={12} sm={10} md={6} lg={4} >
                                    <Box>
                                        <InputLabel >:עיר</InputLabel>
                                        <Select
                                            variant='standard'
                                            className="selectPurple"
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
                                {/* --- Input: communityName--- */}
                                <Grid item xs={12} >
                                    <Box>
                                        <TextField
                                            label="שם הקהילה:"
                                            type='text'
                                            variant='standard'
                                            className="inpPurple"
                                            onChange={e => {
                                                formikProps.setFieldValue('communityName', e.target.value);
                                            }}
                                            value={formikProps.values.communityName}
                                            onBlur={formikProps.handleBlur('communityName')}
                                            error={Boolean(formikProps.errors.communityName)}
                                        />

                                        {formikProps.touched.communityName && formikProps.errors.communityName && (
                                            <Box className="errorMessage">
                                                {formikProps.errors.communityName}
                                            </Box>
                                        )}
                                    </Box>
                                </Grid>


                                {/* ---Input: street--- */}
                                <Grid item xs={12} >
                                    <Box>
                                        <TextField
                                            label="רחוב:"
                                            type='text'
                                            variant='standard'
                                            className="inpPurple"
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

                                {/* ---Input: description--- */}
                                <Grid item xs={12} >
                                    <Box>
                                        <InputLabel id="labelCommunityDiscription">:תאר בקצרה על הקהילה</InputLabel>
                                        <TextField
                                            multiline
                                            maxRows={4}
                                            id="inpCommunityDiscription"
                                            placeholder="תאר בקצרה את מטרת הקהילה"
                                            type='text'
                                            variant='standard'
                                            className="inpAboutClass"
                                            onChange={e => {
                                                formikProps.setFieldValue('description', e.target.value);
                                            }}
                                            value={formikProps.values.description}
                                            onBlur={formikProps.handleBlur('description')}
                                            error={Boolean(formikProps.errors.description)}
                                            inputProps={{ maxLength: 100 }}

                                        // sx={{ marginTop: '15%', height: '70px', width: '60%', textAlign: 'center' }}
                                        />

                                        {formikProps.touched.description && formikProps.errors.description && (
                                            <Box className="errorMessage">
                                                {formikProps.errors.description}
                                            </Box>
                                        )}
                                    </Box>
                                </Grid>


                                <Grid item xs={12} sm={10} md={8}>
                                    <p style={{ direction: 'rtl', textAlign: 'right', fontWeight: 'bold' }}>חשוב לדעת!</p>
                                    <ul style={{ direction: 'rtl', textAlign: 'right' }}>
                                        <li>לאחר אישור יצירת הקהילה אתה המנהל הבלעדי שלה</li>
                                        <li>משתמש יכול לנהל קהילה אחת בלבד</li>
                                    </ul>


                                </Grid>





                                <Grid item xs={12} sm={10} md={8} >
                                    <Button
                                        id='btnCreateCommunity'
                                        type='button'
                                        onClick={formikProps.handleSubmit}
                                    >יצירת קהילה</Button>
                                </Grid>
                            </Grid>
                        </Form>
                    )}
                </Formik>
            </Box>
        </Container>
    );
}
