import {
    Box,
    Grid,
    IconButton,
    InputAdornment,
    TextField,
    Typography,
} from "@mui/material";
import {
    VisibilityOutlined,
    VisibilityOffOutlined
} from '@mui/icons-material';
import { Form, FormikProvider, useFormik } from "formik";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useActions } from "../../../../hooks/useActions";

import imageBackground from "../../../../assets/signup-background.jpg"
import { IResetPassword, ResetPasswordServerError } from "../../types";
import { LoadingButton } from "@mui/lab";
import { ResetPasswordSchema } from "../../validation";


const ResetPassword = () => {
    const { ResetPassword } = useActions();
    const navigate = useNavigate();
    const resetPasswordModel: IResetPassword = { email: '' };

    useEffect(() => {
        document.title = "Forgot password";
    }, []);

    const formik = useFormik({
        initialValues: resetPasswordModel,
        validationSchema: ResetPasswordSchema,
        onSubmit: async (values, { setFieldError }) => {
            try {
                await ResetPassword(values)
            }
            catch (exception) {
                const serverErrors = exception as ResetPasswordServerError;
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
            }

        }
    });

    const { errors, touched, isSubmitting, handleSubmit, getFieldProps } = formik;

    return (
        <Grid container sx={{ height: "100vh" }}>
            <Grid
                item
                xs={false}
                sm={false}
                md={5}
                lg={6}
                sx={{
                    backgroundImage: `url(${imageBackground})`,
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
                    <Typography sx={{ marginTop: "101px" }}>
                        Reset your password
                    </Typography>
                    <FormikProvider value={formik} >
                        <Form autoComplete="off" noValidate onSubmit={handleSubmit} >

                            <Grid container >
                                <Grid item xs={12} sx={{ height: "40px", marginTop: "52px" }}>
                                    <TextField
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
                                <Grid item xs={12} sx={{ marginTop: "75px" }}>
                                    <LoadingButton
                                        color="secondary"
                                        variant="contained"
                                        loading={isSubmitting}
                                        type="submit"
                                    >
                                        Send password reset email
                                    </LoadingButton>
                                </Grid>
                            </Grid>

                        </Form>
                    </FormikProvider>
                </Box>
            </Grid>
        </Grid>
    );
}

export default ResetPassword;