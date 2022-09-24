import { DialogActions, DialogContent, DialogTitle, Grid, IconButton, Typography } from "@mui/material";
import { Close } from "@mui/icons-material";

import * as Yup from 'yup';
import { FC } from "react";
import { useTranslation } from "react-i18next";
import { Form, FormikProvider, useFormik } from "formik";

import { useActions } from "../../../hooks/useActions";

import { toLowerFirstLetter } from "../../../http_comon";
import { ServerError } from "../../../store/types";

import { TextFieldFirstStyle } from "../../../components/TextField/styled";
import { LoadingButtonStyle } from "../../../components/LoadingButton/styled";

import { IResetPasswordEmail } from "./types";
// import { ResetPasswordEmailSchema } from "./validation";

interface Props {
    dialogClose: any,
    changeDialog: any
}

const EmailDialog: FC<Props> = ({ dialogClose, changeDialog }) => {
    const { t } = useTranslation();
    const { SendResetPasswordByEmail } = useActions();

    const resetPasswordModel: IResetPasswordEmail = { email: '' };

    const ResetPasswordEmailSchema = Yup.object().shape({
        email: Yup.string().email().required().label(t('validationProps.email')),
    });

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
                // let message = "Sign up failed! \n";
                // if (serverErrors.status === 400)
                //     message += "Validation failed.";
            }

        }
    });

    const { errors, touched, isSubmitting, handleSubmit, getFieldProps } = formik;

    return (
        <>
            <DialogTitle color="inherit" sx={{ py: "36px" }}>
                <Typography color="inherit" fontSize="30px" align="center" lineHeight="38px">
                    {t('pages.forgotPassword.title')}
                </Typography>
                <IconButton
                    color="inherit"
                    onClick={dialogClose}
                    sx={{
                        position: 'absolute',
                        right: 20,
                        top: 36,
                        borderRadius: "12px"
                    }}
                >
                    <Close />
                </IconButton>
            </DialogTitle>
            <FormikProvider value={formik} >
                <Form autoComplete="off" noValidate onSubmit={handleSubmit} >
                    <DialogContent sx={{ width: "450px", mx: "auto", mt: "34px", p: 0 }}>
                        <Grid container rowSpacing={5}>
                            <Grid item xs={12} >
                                <TextFieldFirstStyle
                                    fullWidth
                                    variant="standard"
                                    type="text"
                                    label={t('validationProps.email')}
                                    {...getFieldProps('email')}
                                    error={Boolean(touched.email && errors.email)}
                                    helperText={touched.email && errors.email}
                                />
                            </Grid>
                        </Grid>
                    </DialogContent>
                    <DialogActions sx={{ pt: "30px", pb: "16px", px: "32px" }}>
                        <LoadingButtonStyle
                            color="secondary"
                            variant="contained"
                            loading={isSubmitting}
                            type="submit"
                            sx={{ width: "auto", fontSize: "20px", lineHeight: "25px", py: "15px", px: "120px", mx: "auto", textTransform: "none" }}
                        >
                            {t('pages.forgotPassword.email.btn')}
                        </LoadingButtonStyle>
                    </DialogActions>
                    <Typography variant='subtitle1' color="inherit" align="center" sx={{ cursor: "pointer", mb: "36px" }}
                        onClick={changeDialog}>
                        {t('pages.forgotPassword.email.resetByPhone')}
                    </Typography>
                </Form>
            </FormikProvider>
        </>
    )
}

export default EmailDialog;