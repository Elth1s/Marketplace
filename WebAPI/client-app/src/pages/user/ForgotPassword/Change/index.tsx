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
import { useEffect, useState } from "react";
import { useNavigate, useParams, useSearchParams } from "react-router-dom";

import imageBackground from "../../../../assets/signup-background.jpg"
import { IResetChangePassword, ResetPasswordServerError } from "../../types";
import { ResetChangePasswordSchema } from "../../validation";
import { useActions } from "../../../../hooks/useActions";
import { LoadingButton } from "@mui/lab";

const ChangePassword = () => {
    const { ResetChangePassword } = useActions();
    const [showPassword, setShowPassword] = useState(false);
    const [showConfirmPassword, setShowConfirmPassword] = useState(false);
    let [searchParams] = useSearchParams();
    let { token } = useParams() as any;
    const navigate = useNavigate();
    const resetChangePasswordModel: IResetChangePassword = { password: '', confirmPassword: '', token: token, userId: '' };

    useEffect(() => {
        document.title = "Change password";
    }, []);

    const formik = useFormik({
        initialValues: resetChangePasswordModel,
        validationSchema: ResetChangePasswordSchema,
        onSubmit: async (values, { setFieldError }) => {

            try {
                values.userId = searchParams.get("userId") ?? ""
                await ResetChangePassword(values)
                navigate("/auth/signin");
            }
            catch (exeption) {
                const serverErrors = exeption as ResetPasswordServerError;
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
                        Change password
                    </Typography>
                    <FormikProvider value={formik} >
                        <Form autoComplete="off" noValidate onSubmit={handleSubmit} >

                            <Grid container >
                                <Grid item xs={12} sx={{ height: "40px", marginTop: "52px" }}>
                                    <TextField
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
                                <Grid item xs={12} sx={{ height: "40px", marginTop: "52px" }}>
                                    <TextField
                                        fullWidth
                                        variant="standard"
                                        autoComplete="confirmPassword"
                                        type={showConfirmPassword ? 'text' : 'password'}
                                        label="Confirm Password"
                                        {...getFieldProps('confirmPassword')}
                                        InputProps={{
                                            endAdornment: (
                                                <InputAdornment position="end">
                                                    <IconButton onClick={handleShowConfirmPassword} edge="end">
                                                        {showConfirmPassword ? <VisibilityOffOutlined /> : <VisibilityOutlined />}
                                                    </IconButton>
                                                </InputAdornment>
                                            )
                                        }}
                                        error={Boolean(touched.confirmPassword && errors.confirmPassword)}
                                        helperText={touched.confirmPassword && errors.confirmPassword}
                                    />
                                </Grid>

                                <Grid item xs={12} sx={{ marginTop: "75px" }}>
                                    <LoadingButton
                                        color="secondary"
                                        variant="contained"
                                        loading={isSubmitting}
                                        type="submit"
                                    >
                                        Change password
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

export default ChangePassword;