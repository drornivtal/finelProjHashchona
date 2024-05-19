
import { Formik, Field, Form, ErrorMessage } from 'formik';
import * as yup from 'yup';

import LogoImage from '../assets/Logo_TheStreets.png';
import { TextField } from '@mui/material';

const apiLogin = 'https://localhost:7053/api/Users/Login';

const communities = [
    { key: 1, value: 'קרית טבעון' },
    { key: 2, value: 'רמת ישי' },
    { key: 3, value: 'שמשית' },
    { key: 4, value: 'רופין-סטודנטים' },
];

const LoginSchema = yup.object().shape({
    community: yup.string().required("שדה זה הינו חובה"),
    phoneNumber: yup.string()
        .required("שדה זה הינו חובה")
        .min(10, "אופס! נראה שפספסת ספרות")
        .matches(/^05\d+$/, 'המספר אינו תקין'),
    password: yup.string()
        .required("שדה זה הינו חובה")
        .min(6, 'הסיסמא אינה תקינה')
        .matches(/^(?=.*[a-z])(?=.*[A-Z]).+$/, 'הסיסמא אינה תקינה')
});

const Login2 = () => {


    const handleFormSubmit = (values, actions) => {
        console.log('Checked Validation!');
        actions.resetForm();
    };

    const Styles = {
        container: {
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
            justifyContent: 'center',
            width: '100%',
            height: '100%',
        },
        imgContainer: {
            flex: 4,
            //paddingTop: '20px',
        },
        formContainer: {
            flex: 8,
            backgroundColor: 'white',
        },
        inputsContainer: {
            flex: 5,
            backgroundColor: 'white',
        },
        btnsContainer: {
            flex: 3,
            backgroundColor: 'white',
            alignItems: 'center',
        },
        inputRow: {
            display: 'flex',
            flexDirection: 'row-reverse',
            alignItems: 'center',
        },
        input: {
            flex: 1.5,
            height: '10%',
            // marginLeft: '10px',
            borderRadius: '20px',
            padding: '10px',
            backgroundColor: '#9274B2',
            opacity: '0.5',
            fontSize: '15px',
            textAlign: 'right',
        },
        label: {
            flex: 1,
            fontSize: '16px',
            fontWeight: '500',
            textAlign: 'right',
        },
        errorText: {
            color: 'crimson',
            fontWeight: 'bold',
            // marginRight: '122px', 
            textAlign: 'right',
        },
        LoginBtn: {
            flex: 1,
            backgroundColor: '#9274B2',
            borderWidth: '1px',
            borderRadius: '18px',
            width: '38%',
            justifyContent: 'center',
            alignItems: 'center',
            marginTop: '25px',
            marginBottom: '10%',
        },
        RegisterBtn: {
            flex: 1,
            width: '88%',
            backgroundColor: '#DABB80',
            borderRadius: '20px',
            justifyContent: 'center',
            alignItems: 'center',
            marginBottom: '10%',
        },
    };

    return (
        <div style={Styles.container}>
            <Formik
                initialValues={{ community: '', phoneNumber: '', password: '' }}
                validationSchema={LoginSchema}
                onSubmit={handleFormSubmit}
            >
                {formikProps => (
                    <Form>
                        <div style={Styles.imgContainer}>
                            <img style={{ flex: 1 }} src={LogoImage} alt="Logo" />
                        </div>

                        <div style={Styles.formContainer}>
                            <div style={Styles.inputsContainer}>

                                <div style={Styles.inputRow}>
                                    <label htmlFor="com" style={Styles.label}>קהילה:</label>
                                    <TextField
                                        type="text"
                                        name="Community"
                                        sx={Styles.input}
                                        placeholder="יש להזין ספרות בלבד"
                                    />
                                    <ErrorMessage name="phoneNumber" component="div" style={Styles.errorText} />
                                </div>

                                <div style={Styles.inputRow}>
                                    <label htmlFor="phoneNumber" style={Styles.label}>מספר נייד:</label>
                                    <Field
                                        type="text"
                                        name="phoneNumber"
                                        style={Styles.input}
                                        placeholder="יש להזין ספרות בלבד"
                                    />
                                    <ErrorMessage name="phoneNumber" component="div" style={Styles.errorText} />
                                </div>

                                <div style={Styles.inputRow}>
                                    <label htmlFor="password" style={Styles.label}>סיסמא:</label>
                                    <Field
                                        type="password"
                                        name="password"
                                        style={Styles.input}
                                        placeholder="6-10 תווים"

                                    />
                                    <ErrorMessage name="password" component="div" style={Styles.errorText} />
                                </div>
                            </div>

                            <div style={Styles.btnsContainer}>
                                <button type="submit" style={Styles.LoginBtn}>
                                    התחברות
                                </button>

                                <button style={Styles.RegisterBtn} onClick={() => navigation.navigate('CommunitiesMap')}>
                                    עדיין לא חבר בשכונה? לחץ כאן
                                </button>
                            </div>
                        </div>
                    </Form>
                )}
            </Formik>
        </div>
    );
};

export default Login;
