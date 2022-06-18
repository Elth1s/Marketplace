import {
    Box,
    Grid,
    IconButton,
    InputAdornment,
} from "@mui/material";
import {
    VisibilityOutlined,
    VisibilityOffOutlined
} from '@mui/icons-material';
import { Form, FormikProvider, useFormik } from "formik";
import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useGoogleReCaptcha } from "react-google-recaptcha-v3";
// import { toast } from 'react-toastify';

import GoogleExternalLogin from "../../../components/Google";

import { SignUpSchema } from "../validation";
import { useActions } from "../../../hooks/useActions";
import { IRegisterModel } from "../types";
import { ServerError } from "../../../store/types";
import { AuthAvatar, AuthHeaderTypography, AuthLoadingButton, AuthSideTypography, AuthTextField } from "../styled";

import { signup } from "../../../assets/backgrounds"
import { twitter, facebook } from "../../../assets/icons";

const SignUpPage = () => {
    const { RegisterUser } = useActions();
    const { executeRecaptcha } = useGoogleReCaptcha();

    const [showPassword, setShowPassword] = useState(false);
    const [showConfirmPassword, setShowConfirmPassword] = useState(false);
    const navigate = useNavigate();
    const registerModel: IRegisterModel = { firstName: '', secondName: '', email: '', password: '' };

    useEffect(() => {
        document.title = "Sign up";
    }, []);

    const formik = useFormik({
        initialValues: registerModel,
        validationSchema: SignUpSchema,
        onSubmit: async (values, { setFieldError }) => {
            if (!executeRecaptcha) {
                // toast.error("Captcha validation error");
                return;
            }
            const reCaptchaToken = await executeRecaptcha();
            try {
                await RegisterUser(values, reCaptchaToken);
                navigate("/");
                // toast.success('Sign up success!');
            }
            catch (exeption) {
                const serverErrors = exeption as ServerError;
                if (serverErrors.errors)
                    Object.entries(serverErrors.errors).forEach(([key, value]) => {
                        if (Array.isArray(value)) {
                            let message = "";
                            value.forEach((item) => {
                                message += `${item} `;
                            });
                            setFieldError(key.toLowerCase(), message);
                        }
                    });
                let message = "Sign up failed! \n";
                if (serverErrors.status === 400)
                    message += "Validation failed.";
                // toast.error(message, { position: "top-right" });
            }

        }
    });

    const { errors, touched, isSubmitting, handleSubmit, getFieldProps } = formik;

    const handleShowPassword = () => {
        setShowPassword((show) => !show);
    };
    const handleShowConfirmPassword = () => {
        setShowConfirmPassword((show) => !show);
    };
    return (
        <Grid container sx={{ height: "100vh" }}>
            <Grid
                item
                xs={false}
                sm={false}
                md={5}
                lg={6}
                sx={{
                    backgroundImage: `url(${signup})`,
                    backgroundRepeat: 'no-repeat',
                    backgroundSize: 'cover',
                    backgroundPosition: 'center',
                }}
            />
            <Grid item xs={12} sm={8} md={6}
                sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center'
                }}>
                <Box
                    sx={{
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                        justifyContent: 'center',
                        width: "500px"
                    }}>
                    <AuthHeaderTypography sx={{ marginTop: "57px" }}>
                        Зареєструйтесь
                    </AuthHeaderTypography>
                    <FormikProvider value={formik} >
                        <Form autoComplete="off" noValidate onSubmit={handleSubmit} >

                            <Grid container >
                                <Grid item xs={12} sx={{ height: "40px", marginTop: "76px" }}>
                                    <AuthTextField
                                        fullWidth
                                        variant="standard"
                                        autoComplete="firstName"
                                        type="text"
                                        label="First Name"
                                        {...getFieldProps('firstName')}
                                        error={Boolean(touched.firstName && errors.firstName)}
                                        helperText={touched.firstName && errors.firstName}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{ height: "40px", marginTop: "55px" }}>
                                    <AuthTextField
                                        fullWidth
                                        variant="standard"
                                        autoComplete="secondName"
                                        type="text"
                                        label="Second Name"
                                        {...getFieldProps('secondName')}
                                        error={Boolean(touched.secondName && errors.secondName)}
                                        helperText={touched.secondName && errors.secondName}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{ height: "40px", marginTop: "55px" }}>
                                    <AuthTextField
                                        fullWidth
                                        variant="standard"
                                        autoComplete="email"
                                        type="email"
                                        label="Email address"
                                        {...getFieldProps('email')}
                                        error={Boolean(touched.email && errors.email)}
                                        helperText={touched.email && errors.email}
                                    />
                                </Grid>

                                <Grid item xs={12} sx={{ height: "40px", marginTop: "55px" }}>
                                    <AuthTextField
                                        fullWidth
                                        variant="standard"
                                        autoComplete="password"
                                        type={showPassword ? 'text' : 'password'}
                                        label="Password"
                                        {...getFieldProps('password')}
                                        InputProps={{
                                            endAdornment: (
                                                <InputAdornment position="end">
                                                    <IconButton onClick={handleShowPassword} edge="end">
                                                        {showPassword ? <VisibilityOffOutlined /> : <VisibilityOutlined />}
                                                    </IconButton>
                                                </InputAdornment>
                                            )
                                        }}
                                        error={Boolean(touched.password && errors.password)}
                                        helperText={touched.password && errors.password}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{ marginTop: "20px" }} display="flex" justifyContent="flex-end">
                                    <AuthSideTypography component={Link} to="/auth/signin" sx={{ cursor: "pointer", textDecoration: "none", color: "#000" }} >Маєте акаунт?</AuthSideTypography>
                                </Grid>
                                <Grid item xs={12} sx={{ marginTop: "62px" }}>
                                    <AuthLoadingButton
                                        color="secondary"
                                        variant="contained"
                                        loading={isSubmitting}
                                        type="submit"
                                    >
                                        Реєстрація
                                    </AuthLoadingButton>
                                </Grid>
                                <Grid item xs={12} sx={{ marginTop: "66px" }} display="flex" justifyContent="center" >
                                    <Box sx={{ width: "98px", height: "17px", borderBottom: "2px solid #000" }} />
                                    <AuthSideTypography sx={{ padding: "0 7px" }}>або</AuthSideTypography>
                                    <Box sx={{ width: "98px", height: "17px", borderBottom: "2px solid #000" }} />
                                </Grid>
                                <Grid item xs={12} sx={{ marginTop: "46px" }} display="flex" justifyContent="center" >
                                    <GoogleExternalLogin />
                                    <AuthAvatar src={facebook} sx={{ marginX: "40px" }}>F</AuthAvatar>
                                    <AuthAvatar src={twitter} />
                                </Grid>

                            </Grid>

                        </Form>
                    </FormikProvider>
                </Box>
            </Grid>
        </Grid>
    );
}

export default SignUpPage;