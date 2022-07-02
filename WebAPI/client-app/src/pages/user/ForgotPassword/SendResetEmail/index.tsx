import {
    Box,
    Grid,
    TextField,
    Typography,
} from "@mui/material";
import { Form, FormikProvider, useFormik } from "formik";
import { useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useActions } from "../../../../hooks/useActions";

import { signup } from "../../../../assets/backgrounds"

import { IResetPasswordEmail } from "../../types";
import { LoadingButton } from "@mui/lab";
import { ResetPasswordEmailSchema } from "../../validation";
import { AuthSideTypography } from "../../../auth/styled";
import { ServerError } from "../../../../store/types";
import { toLowerFirstLetter } from "../../../../http_comon";


const SendResetPasswordEmail = () => {
    const { SendResetPasswordByEmail } = useActions();
    const navigate = useNavigate();
    const resetPasswordModel: IResetPasswordEmail = { email: '' };

    useEffect(() => {
        document.title = "Forgot password";
    }, []);

    const formik = useFormik({
        initialValues: resetPasswordModel,
        validationSchema: ResetPasswordEmailSchema,
        onSubmit: async (values, { setFieldError }) => {
            try {
                await SendResetPasswordByEmail(values)
            }
            catch (exception) {
                const serverErrors = exception as ServerError;
                if (serverErrors.errors)
                    Object.entries(serverErrors.errors).forEach(([key, value]) => {
                        if (Array.isArray(value)) {
                            let message = "";
                            value.forEach((item) => {
                                message += `${item} `;
                            });
                            setFieldError(toLowerFirstLetter(key), message);
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
                    <Typography sx={{ marginTop: "101px" }} variant="h3">
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
                                <Grid item xs={12} sx={{ marginTop: "42px" }} display="flex" justifyContent="flex-end">
                                    <AuthSideTypography component={Link} to="/resetPasswordPhone" sx={{ cursor: "pointer", textDecoration: "none", color: "#000" }} >Reset by phone</AuthSideTypography>
                                </Grid>
                                <Grid item xs={12} sx={{ marginTop: "45px" }}>
                                    <LoadingButton
                                        color="secondary"
                                        variant="contained"
                                        loading={isSubmitting}
                                        type="submit"
                                        size="large"
                                    >
                                        Send email
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

export default SendResetPasswordEmail;