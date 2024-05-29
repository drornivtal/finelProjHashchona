
import { useState } from "react";

import { Box, Button, Container, Grid, InputLabel, TextField } from "@mui/material";
import { DatePicker, LocalizationProvider, TimePicker } from "@mui/x-date-pickers";

import { Formik, Form } from "formik";
import { newRequestSchema } from "../../utilities/YupSchemas";
import { categoriesList, iconMapping } from "../../utilities/ListsUtilities";

import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';

import LogoImage from '../../assets/Logo_TheStreets.png';
import '../../styles/NewRequestStyles.css';
import '../../styles/RegisterStyles.css';
import '../../styles/LoginStyles.css';
import { postAndPutReqFunction } from "../../utilities/ApiUtilities";


export default function NewRequest() {

    const apiNewReq = 'https://proj.ruppin.ac.il/cgroup62/test2/tar1/api/RequestsForHelp/postNewReq';

    const [selectedCategory, setSelectedCategory] = useState(null);
    const user = JSON.parse(localStorage.getItem('user'));
    const communityId = JSON.parse(localStorage.getItem('userCommunityId'));

    const handleIconClick = (category, formikProps) => {
        if (selectedCategory === category) {
            setSelectedCategory(null);
            formikProps.setFieldValue('category', null);
        } else {
            setSelectedCategory(category);
            formikProps.setFieldValue('category', category);
        }
    };

    const handleFormSubmit = (values, actions) => {

        const dueDate = new Date(values.dueDate);
        const dueTime = new Date(values.dueTime);

        dueDate.setHours(dueTime.getHours(), dueTime.getMinutes() + 180);

        let newRequest = {
            reqID: 0,
            categoryId: values.category,
            dueDate: dueDate.toISOString(),
            dueTime: "string",
            postDate: "2024-05-22T18:32:43.906Z",
            postTime: "string",
            description: values.description,
            gotHelp: false,
            userReqID: user.userId,
            communityID: communityId
        }

        console.log("this is a newRequest : ");
        console.log(newRequest);
        postAndPutReqFunction(newRequest, apiNewReq, 'POST');
        actions.resetForm();
    };

    return (
        <Container sx={{ display: 'flex', flexDirection: 'column', height: '90vh', width: '80vw' }}>
            <Box
                sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    justifyContent: 'flex-start',
                    alignItems: 'center',
                    height: '20vh',
                    width: '100%',
                    // backgroundColor: 'greenyellow',

                }}
            >
                <img
                    src={LogoImage}
                    alt="Logo"
                    style={{
                        maxWidth: '100%',
                        maxHeight: '100%',
                    }}
                />
            </Box>


            {/* <Box sx={{height: '80%',backgroundColor: 'orange' }} > */}
            <Box sx={{ height: '70vh', width: '100%' }} >
                <Formik
                    initialValues={{
                        category: null,
                        dueDate: null,
                        dueTime: null,
                        description: ''
                    }}
                    validationSchema={newRequestSchema}
                    onSubmit={(values, actions) => {
                        handleFormSubmit(values, actions);
                        console.log("Check Submit!!");
                    }}
                >
                    {formikProps => (

                        <Form>

                            <Box
                                sx={{
                                    backgroundColor: 'lightgray',
                                    opacity: 0.8,
                                    marginBottom: '1%',
                                    // paddingTop: '2%',
                                    // borderEndEndRadius:'15px',
                                    // borderEndStartRadius:'15px',
                                    borderRadius: '10px',
                                    height: '10%',
                                    width: '100%',
                                    display: 'flex',
                                    overflowX: 'auto',  // Enable horizontal scrolling
                                    whiteSpace: 'nowrap',  // Ensure the children stay in a single line
                                    direction: 'rtl',
                                    '&::-webkit-scrollbar': {
                                        display: 'none',  //  hide scrollbar for Webkit browsers
                                    },
                                }}>

                                {
                                    categoriesList.map((category) => {
                                        const IconComponent = iconMapping[category.IconName];
                                        const isSelected = selectedCategory === category.key;

                                        return (

                                            <Button
                                                key={category.key}
                                                onClick={() => handleIconClick(category.key, formikProps)}
                                                sx={{
                                                    margin: '3%',
                                                    bgcolor: '#9274B2',
                                                    // opacity: 1,
                                                    opacity: isSelected ? 0.6 : 1,
                                                    borderRadius: 15,
                                                    width: '50px',
                                                    height: '50px',
                                                    minWidth: '50px',
                                                    padding: '0',
                                                }}

                                            >
                                                {IconComponent ? (

                                                    <IconComponent id={category.key} sx={{ color: 'white', fontSize: '28px' }} />

                                                ) : null}
                                            </Button>
                                        );
                                    })
                                }

                            </Box>

                            <Grid container rowSpacing={4} justifyContent='center' paddingTop={1}>

                                {/* <InputLabel>:תאריך ושעה</InputLabel> */}

                                <Grid item xs={7} md={12} >

                                    <Box>
                                        <LocalizationProvider dateAdapter={AdapterDayjs}>
                                            <DatePicker
                                                sx={{ backgroundColor: '#9274B2', opacity: 0.7 }}
                                                label="תאריך"
                                                variant='standard'
                                                format='DD/MM/YY'
                                                // className='selectPurple'
                                                value={formikProps.values.dueDate}
                                                onChange={userDate => {
                                                    formikProps.setFieldValue('dueDate', userDate);
                                                }}
                                                onBlur={formikProps.handleBlur('dueDate')}
                                                error={Boolean(formikProps.errors.dueDate)}
                                            />
                                        </LocalizationProvider>

                                        {formikProps.touched.dueDate && formikProps.errors.dueDate && (
                                            <Box className="errorMessage">
                                                {formikProps.errors.dueDate}
                                            </Box>
                                        )}
                                    </Box>
                                </Grid>

                                <Grid item xs={4} md={12} >
                                    <Box>
                                        <LocalizationProvider dateAdapter={AdapterDayjs}>
                                            <TimePicker
                                                sx={{ backgroundColor: '#9274B2', opacity: 0.7 }}
                                                label="שעה"
                                                variant='standard'
                                                format='hh:mm'
                                                // className='inpYellow'
                                                value={formikProps.values.dueTime}
                                                onChange={userDate => {
                                                    formikProps.setFieldValue('dueTime', userDate);
                                                }}
                                                onBlur={formikProps.handleBlur('dueTime')}
                                                error={Boolean(formikProps.errors.dueTime)}
                                            />
                                        </LocalizationProvider>

                                        {formikProps.touched.dueTime && formikProps.errors.dueTime && (
                                            <Box className="errorMessage">
                                                {formikProps.errors.dueTime}
                                            </Box>
                                        )}
                                    </Box>
                                </Grid>


                                {/* ---Input: description--- */}
                                <Grid item xs={12} >
                                    <Box>
                                        {/* <InputLabel>:תיאור קצר</InputLabel> */}
                                        <TextField
                                            multiline
                                            maxRows={4}
                                            id="inpRequestDiscription"
                                            placeholder="תאר בקצרה את הסיוע שאתה זקוק לו"
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
                                        {formikProps.touched.category && formikProps.errors.category && (
                                            <Box className="errorMessage" sx={{ marginTop: '1%', }}>
                                                {formikProps.errors.category}
                                            </Box>
                                        )}

                                    </Box>
                                </Grid>

                                <Grid item xs={12} sm={10} md={8} >
                                    <Button
                                        id="btnCreateNewRequest"
                                        type='button'
                                        onClick={formikProps.handleSubmit}
                                    > !יאלה, לעזור</Button>
                                </Grid>
                            </Grid>
                        </Form>
                    )}
                </Formik>
            </Box>

        </Container>
    )
}
