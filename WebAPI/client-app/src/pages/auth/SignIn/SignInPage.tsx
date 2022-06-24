import {
    Box,
    Grid,
    IconButton,
    InputAdornment,
} from "@mui/material";
import { AuthLoadingButton, AuthHeaderTypography, AuthSideTypography, AuthAvatar, AuthTextField } from "../styled";
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


import { LogInSchema } from "../validation";
import { useActions } from "../../../hooks/useActions";
import { ILoginModel } from "../types";
import { ServerError } from "../../../store/types";

import { login } from "../../../assets/backgrounds";
import { twitter, facebook } from "../../../assets/icons";
import { getLocalAccessToken } from "../../../http_comon";

const SignIn = () => {
    const { LoginUser, AuthUser } = useActions();
    const { executeRecaptcha } = useGoogleReCaptcha();

    const [showPassword, setShowPassword] = useState(false);
    const navigate = useNavigate();
    const loginModel: ILoginModel = { email: '', password: '' };

    useEffect(() => {
        document.title = "SignIn";
    }, []);

    const formik = useFormik({
        initialValues: loginModel,
        validationSchema: LogInSchema,
        onSubmit: async (values, { setFieldError }) => {
            if (!executeRecaptcha) {
                // toast.error("Captcha validation error");
                return;
            }
            const reCaptchaToken = await executeRecaptcha();
            try {
                await LoginUser(values, reCaptchaToken);
                navigate("/");
                // toast.success('Login Success!');
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
                let message = "Login failed! \n";
                if (serverErrors.status === 400)
                    message += "Validation failed.";
                if (serverErrors.status === 401)
                    message += "The user with the entered data does not exist.";
                // toast.error(message, { position: "top-right" });
            }

        }
    });

    const { errors, touched, isSubmitting, handleSubmit, getFieldProps } = formik;

    const handleShowPassword = () => {
        setShowPassword((show) => !show);
    };

    return (
        <Grid container sx={{ height: "100vh" }}>
            <Grid
                item
                xs={false}
                sm={4}
                md={6}
                sx={{
                    backgroundImage: `url(${login})`,
                    backgroundRepeat: 'no-repeat',
                    backgroundSize: 'cover',
                    backgroundPosition: 'center'
                }}
            />
            <Grid item xs={12} sm={8} md={6}
                sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                    minHeight: "100%"
                }}>
                <Box
                    sx={{
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                        justifyContent: 'center',
                        width: "500px"
                    }}>

                    <AuthHeaderTypography sx={{ marginTop: "98px" }}>
                        Sign In
                    </AuthHeaderTypography>
                    <FormikProvider value={formik} >
                        <Form autoComplete="off" noValidate onSubmit={handleSubmit} >

                            <Grid container>
                                <Grid item xs={12} sx={{ height: "40px", marginTop: "92px" }}>
                                    <AuthTextField
                                        fullWidth
                                        variant="standard"
                                        type="email"
                                        label="Email address"
                                        {...getFieldProps('email')}
                                        error={Boolean(touched.email && errors.email)}
                                        helperText={touched.email && errors.email}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{ height: "40px", marginTop: "110px" }}>
                                    <AuthTextField
                                        fullWidth
                                        variant="standard"
                                        type={showPassword ? 'text' : 'password'}
                                        label="Password"
                                        {...getFieldProps('password')}
                                        InputProps={{
                                            endAdornment: (
                                                <InputAdornment position="end">
                                                    <IconButton sx={{ padding: "3px" }} onClick={handleShowPassword} edge="start">
                                                        {showPassword ? <VisibilityOffOutlined /> : <VisibilityOutlined />}
                                                    </IconButton>
                                                </InputAdornment>
                                            )
                                        }}
                                        error={Boolean(touched.password && errors.password)}
                                        helperText={touched.password && errors.password}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{ marginTop: "62px", width: "100%", display: "flex", justifyContent: "space-between" }} >
                                    <AuthSideTypography component={Link} to="/auth/signup" sx={{ cursor: "pointer", textDecoration: "none", color: "#000" }} >Don't have an account?</AuthSideTypography>
                                    <AuthSideTypography component={Link} to="/resetPassword" sx={{ cursor: "pointer", textDecoration: "none", color: "#000" }} >Forgot password?</AuthSideTypography>
                                </Grid>
                                <Grid item xs={12} sx={{ marginTop: "85px" }}>
                                    <AuthLoadingButton
                                        color="primary"
                                        variant="contained"
                                        loading={isSubmitting}
                                        type="submit"
                                    >
                                        Sign In
                                    </AuthLoadingButton>
                                </Grid>
                                <Grid item xs={12} sx={{ marginTop: "60px" }} display="flex" justifyContent="center" >
                                    <Box sx={{ width: "98px", height: "17px", borderBottom: "2px solid #000" }} />
                                    <AuthSideTypography sx={{ padding: "0 7px" }}>or</AuthSideTypography>
                                    <Box sx={{ width: "98px", height: "17px", borderBottom: "2px solid #000" }} />
                                </Grid>
                                <Grid item xs={12} sx={{ position: "relative", marginTop: "45px" }} display="flex" justifyContent="center" >
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

export default SignIn;