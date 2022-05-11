import {
    Box,
    Grid,
    IconButton,
    InputAdornment,
    TextField,
    Typography
} from "@mui/material";
import { Visibility, VisibilityOff } from '@mui/icons-material';
import { LoadingButton } from '@mui/lab';
import { Form, FormikProvider, useFormik } from "formik";
import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useGoogleReCaptcha } from "react-google-recaptcha-v3";
// import { toast } from 'react-toastify';


import { LogInSchema } from "../validation";
import { useActions } from "../../../hooks/useActions";
import { ILoginModel, LoginServerError } from "../types";
import GoogleSignIn from "../Google/GoogleSignIn";


const SignIn = () => {
    const { LoginUser } = useActions();
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
                console.log("qwe")
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
                const serverErrors = exeption as LoginServerError;
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
        <Grid container sx={{ height: '100vh' }}>
            <Grid
                item
                xs={false}
                sm={4}
                md={7}
                sx={{
                    backgroundImage: 'url(https://source.unsplash.com/featured/?architecture)',
                    backgroundRepeat: 'no-repeat',
                    backgroundSize: 'cover',
                    backgroundPosition: 'center',
                }}
            />
            <Grid item xs={12} sm={8} md={5}>
                <Box
                    sx={{
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                        justifyContent: 'center',
                        height: "90vh",
                    }}
                >
                    <Link to="/" >
                        <svg width="270" viewBox="0 0 350 195.37059330623254"><defs id="SvgjsDefs1809"></defs><g id="SvgjsG1810" transform="matrix(2.1836736706906477,0,0,2.1836736706906477,72.7213351140785,-37.69239053006673)" fill="#66fcf1"><path xmlns="http://www.w3.org/2000/svg" d="M46.769,20.261c0.547,0,1.095,0.181,1.513,0.542l12.837,11.105c0.306,0.264,0.59,0.39,0.827,0.39  c0.413,0,0.686-0.381,0.686-1.082v-2.679c0-1.104,0.896-2,2-2h4.995c1.104,0,2,0.896,2,2v10.46c0,1.104,0.677,2.586,1.513,3.309  l6.235,5.394c0.836,0.723,0.617,1.309-0.487,1.309h-5.261c-1.104,0-2,0.793-2,1.771s0,1.771,0,1.771v26.289c0,1.104-0.896,2-2,2  H23.912c-1.104,0-2-0.896-2-2v-27.83c0-1.104-0.896-2-2-2h-5.261c-1.104,0-1.323-0.586-0.487-1.309l31.093-26.897  C45.674,20.441,46.222,20.261,46.769,20.261 M46.769,17.261c-1.291,0-2.525,0.452-3.475,1.272L12.201,45.432  c-2.044,1.767-1.537,3.692-1.333,4.24c0.204,0.549,1.08,2.337,3.783,2.337h4.261v26.83c0,2.757,2.243,5,5,5h45.715  c2.757,0,5-2.243,5-5V52.55v-0.541h4.261c2.703,0,3.579-1.788,3.783-2.337c0.204-0.548,0.711-2.474-1.334-4.241l-6.234-5.394  c-0.19-0.18-0.464-0.779-0.476-1.046V28.537c0-2.757-2.243-5-5-5h-4.995c-2.182,0-4.042,1.405-4.723,3.357l-9.664-8.36  C49.294,17.713,48.06,17.261,46.769,17.261L46.769,17.261z"></path></g><g id="SvgjsG1811" transform="matrix(1.0545345177353658,0,0,1.0545345177353658,-2.952695845113036,152.7674022090442)" fill="#f1f1f1"><path d="M18.6 31.2 l0.4 0 l-0.6 -7.2 l0 -12 l5.2 0 l0 28 l-5.4 0 l-10.4 -19.2 l-0.4 0 l0.6 7.2 l0 12 l-5.2 0 l0 -28 l5.4 0 z M44.675 11.600000000000001 c4 0 6.6 2.6 6.6 6.2 l0 16.4 c0 3.6 -2.6 6.2 -6.6 6.2 l-8.8 0 c-4 0 -6.6 -2.6 -6.6 -6.2 l0 -16.4 c0 -3.6 2.6 -6.2 6.6 -6.2 l8.8 0 z M37.275000000000006 16 c-1.6 0 -2.8 1.2 -2.8 2.6 l0 14.8 c0 1.4 1.2 2.6 2.8 2.6 l6 0 c1.6 0 2.8 -1.2 2.8 -2.6 l0 -14.8 c0 -1.4 -1.2 -2.6 -2.8 -2.6 l-6 0 z M72.75 31.2 l0.4 0 l-0.6 -7.2 l0 -12 l5.2 0 l0 28 l-5.4 0 l-10.4 -19.2 l-0.4 0 l0.6 7.2 l0 12 l-5.2 0 l0 -28 l5.4 0 z M97.42500000000001 12 l8.6 25.8 l0 2.2 l-5.2 0 l-1.8 -6 l-10 0 l-1.8 6 l-5.2 0 l0 -2.2 l8.6 -25.8 l6.8 0 z M93.82500000000002 17.6 l-3.6 12 l7.6 0 l-3.6 -12 l-0.4 0 z M127.70000000000002 36.2 l-4.8 0 l-6.2 -16.2 l-0.4 0 l-2 20 l-5.4 0 l0 -2.2 l3.4 -25.8 l6.2 0 l6.6 17.2 l0.4 0 l6.6 -17.2 l6.2 0 l3.4 25.8 l0 2.2 l-5.4 0 l-2 -20 l-0.4 0 z M146.77500000000003 40 l0 -28 l19.2 0 l0 3 l-1.4 1.4 l-12.6 0 l0 7.2 l10.2 0 l0 4.4 l-10.2 0 l0 7.6 l14 0 l0 4.4 l-19.2 0 z M183.05000000000004 27.8 l-7.4 0 l0 7.8 l7.4 0 c2 0 3.2 -1.2 3.2 -2.8 l0 -2.2 c0 -1.6 -1.2 -2.8 -3.2 -2.8 z M186.65000000000003 25 l0 0.4 s4.8 0.6 4.8 5.8 l0 2.8 c0 3.2 -2.8 6 -6.8 6 l-14.2 0 l0 -28 l13 0 c4 0 6.8 2.8 6.8 6 l0 2.6 c0 3.4 -3.6 4.4 -3.6 4.4 z M181.85000000000002 16.4 l-6.2 0 l0 7 l6.2 0 c2 0 3.2 -1.2 3.2 -2.8 l0 -1.4 c0 -1.6 -1.2 -2.8 -3.2 -2.8 z M211.12500000000003 11.600000000000001 c4 0 6.6 2.6 6.6 6.2 l0 16.4 c0 3.6 -2.6 6.2 -6.6 6.2 l-8.8 0 c-4 0 -6.6 -2.6 -6.6 -6.2 l0 -16.4 c0 -3.6 2.6 -6.2 6.6 -6.2 l8.8 0 z M203.72500000000002 16 c-1.6 0 -2.8 1.2 -2.8 2.6 l0 14.8 c0 1.4 1.2 2.6 2.8 2.6 l6 0 c1.6 0 2.8 -1.2 2.8 -2.6 l0 -14.8 c0 -1.4 -1.2 -2.6 -2.8 -2.6 l-6 0 z M238.00000000000003 11.600000000000001 c4 0 6.6 2.6 6.6 6.2 l0 16.4 c0 3.6 -2.6 6.2 -6.6 6.2 l-8.8 0 c-4 0 -6.6 -2.6 -6.6 -6.2 l0 -16.4 c0 -3.6 2.6 -6.2 6.6 -6.2 l8.8 0 z M230.60000000000002 16 c-1.6 0 -2.8 1.2 -2.8 2.6 l0 14.8 c0 1.4 1.2 2.6 2.8 2.6 l6 0 c1.6 0 2.8 -1.2 2.8 -2.6 l0 -14.8 c0 -1.4 -1.2 -2.6 -2.8 -2.6 l-6 0 z M255.47500000000002 12 l0 11.6 l5 0 l5.4 -11.6 l5.2 0 l0 2.2 l-6.4 11.6 l6.8 12 l0 2.2 l-5.2 0 l-5.8 -12 l-5 0 l0 12 l-5.2 0 l0 -28 l5.2 0 z M281.15000000000003 40 l-5.2 0 l0 -28 l5.2 0 l0 28 z M303.42500000000007 31.2 l0.4 0 l-0.6 -7.2 l0 -12 l5.2 0 l0 28 l-5.4 0 l-10.4 -19.2 l-0.4 0 l0.6 7.2 l0 12 l-5.2 0 l0 -28 l5.4 0 z M319.3 19 l0 14 c0 1.4 1.2 2.6 2.8 2.6 l7.4 0 l0 -11 l5.2 0 l0 15.4 l-14 0 c-4 0 -6.6 -2.6 -6.6 -6.2 l0 -15.6 c0 -3.6 2.6 -6.2 6.6 -6.2 l13 0 l0 3 l-1.4 1.4 l-10.2 0 c-1.6 0 -2.8 1.2 -2.8 2.6 z"></path></g></svg>
                    </Link>
                    <Typography component="h1" variant="h4" mt={1.5}>
                        Sign In
                    </Typography>
                    <Box sx={{ mt: 1 }} >
                        <FormikProvider value={formik} >
                            <Form autoComplete="off" noValidate onSubmit={handleSubmit} >

                                <Grid sx={{ p: 5, }} container spacing={4}>
                                    <Grid item xs={8} mx="auto">
                                        <TextField
                                            fullWidth
                                            margin="normal"
                                            autoComplete="username"
                                            type="email"
                                            label="Email address"
                                            variant="outlined"
                                            {...getFieldProps('email')}
                                            error={Boolean(touched.email && errors.email)}
                                            helperText={touched.email && errors.email}
                                        />
                                    </Grid>
                                    <Grid item xs={8} mx="auto">
                                        <TextField
                                            fullWidth
                                            autoComplete="current-password"
                                            type={showPassword ? 'text' : 'password'}
                                            label="Password"
                                            variant="outlined"
                                            {...getFieldProps('password')}
                                            InputProps={{
                                                endAdornment: (
                                                    <InputAdornment position="end">
                                                        <IconButton onClick={handleShowPassword} edge="end">
                                                            {showPassword ? <Visibility /> : <VisibilityOff />}
                                                        </IconButton>
                                                    </InputAdornment>
                                                )
                                            }}
                                            error={Boolean(touched.password && errors.password)}
                                            helperText={touched.password && errors.password}
                                        />
                                    </Grid>
                                    <Grid item xs={8} mx="auto">
                                        <GoogleSignIn />
                                    </Grid>
                                    <Grid item xs={12} mt={3} display="flex" justifyContent="center" >
                                        <LoadingButton
                                            sx={{ paddingX: "35px" }}
                                            size="large"
                                            type="submit"
                                            variant="contained"
                                            loading={isSubmitting}
                                        >
                                            Sign In
                                        </LoadingButton>
                                    </Grid>
                                    <Grid item xs={10} mt={3} display="flex" justifyContent="flex-end">
                                        <Typography >
                                            Don’t have an account? <Typography component={Link} variant="h6" style={{ textDecoration: 'none' }} to="/auth/signup">Sign Up.</Typography>
                                        </Typography>
                                    </Grid>
                                </Grid>
                            </Form>
                        </FormikProvider>
                    </Box>
                </Box>
            </Grid>
        </Grid>
    );
}

export default SignIn;