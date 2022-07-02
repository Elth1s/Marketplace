import {
    Box,
    Button,
    Grid,
    Stack,
    TextField,
    Typography,
} from "@mui/material";
import { Form, FormikProvider, useFormik } from "formik";
import { useEffect, useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { useActions } from "../../../../hooks/useActions";

import { signup } from "../../../../assets/backgrounds"
import { AuthSideTypography } from "../../../auth/styled";
import { IResetPasswordInfo, IResetPasswordPhone } from "../../types";
import { LoadingButton } from "@mui/lab";
import { ResetPasswordPhoneSchema } from "../../validation";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";
import { ServerError } from "../../../../store/types";


const SendResetPasswordPhone = () => {
    const { SendResetPasswordByPhoneCode, ValidateCodeForResetPasswordByPhone } = useActions();
    const navigate = useNavigate();
    const [isCodeSend, setIsCodeSend] = useState(false);
    const resetPasswordModel: IResetPasswordPhone = { phone: '', code: '' };
    useEffect(() => {
        document.title = "Forgot password";
    }, []);

    const formik = useFormik({
        initialValues: resetPasswordModel,
        validationSchema: ResetPasswordPhoneSchema,
        onSubmit: async (values, { setFieldError }) => {
            try {
                var res = await ValidateCodeForResetPasswordByPhone(values) as unknown as IResetPasswordInfo
                navigate(`/resetPassword/${res.token}?userId=${res.userId}`);
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
                            setFieldError(key.toLowerCase(), message);
                        }
                    });
                let message = "Sign up failed! \n";
                if (serverErrors.status === 400)
                    message += "Validation failed.";
            }

        }
    });

    const onClickSendCodeBtn = async (props: any) => {
        try {
            await SendResetPasswordByPhoneCode({ phone: formik.values.phone });
            setIsCodeSend(true);
        }
        catch (extection) {
        }
    };

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
                    <Box sx={{ mt: 3 }}>
                        <FormikProvider value={formik} >
                            <Form noValidate onSubmit={handleSubmit} >
                                <Stack direction="row">
                                    <Grid container spacing={4}>
                                        <>
                                            <Grid item xs={12} md={8} py={3}>
                                                <TextField
                                                    fullWidth
                                                    variant="standard"
                                                    autoComplete="phone"
                                                    type="tel"
                                                    label="Phone number"
                                                    {...getFieldProps('phone')}
                                                    error={Boolean(touched.phone && errors.phone)}
                                                    helperText={touched.phone && errors.phone}
                                                />
                                            </Grid>
                                            <Grid item xs={12} md={4} display="flex" justifyContent="flex-end">
                                                <Button
                                                    sx={{ alignSelf: "center" }}
                                                    size="large"
                                                    variant="contained" onClick={onClickSendCodeBtn}>
                                                    Send code
                                                </Button>
                                            </Grid>
                                        </>
                                        <Grid item xs={12} display="flex" justifyContent="flex-end">
                                            <AuthSideTypography component={Link} to="/resetPasswordEmail" sx={{ cursor: "pointer", textDecoration: "none", color: "#000" }} >Reset by email</AuthSideTypography>
                                        </Grid>
                                        <Grid item xs={12} sx={{ height: "40px", marginTop: "52px" }}
                                            visibility={isCodeSend ? "visible" : "hidden"}>
                                            <TextField
                                                fullWidth
                                                variant="standard"
                                                autoComplete="code"
                                                type="text"
                                                label="Code"
                                                {...getFieldProps('code')}
                                                error={Boolean(touched.code && errors.code)}
                                                helperText={touched.code && errors.code}
                                            />
                                        </Grid>
                                        <Grid item xs={12} sx={{ marginTop: "75px" }} visibility={isCodeSend ? "visible" : "hidden"}>
                                            <LoadingButton
                                                color="secondary"
                                                variant="contained"
                                                loading={isSubmitting}
                                                type="submit"
                                                size="large"
                                            >
                                                Validate code
                                            </LoadingButton>
                                        </Grid>
                                    </Grid>
                                </Stack>
                            </Form>
                        </FormikProvider>
                    </Box>
                </Box>
            </Grid>
        </Grid >
    );
}

export default SendResetPasswordPhone;