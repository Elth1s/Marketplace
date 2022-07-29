import { DialogActions, DialogContent, DialogTitle, Grid, IconButton, Typography } from "@mui/material";
import { Close } from "@mui/icons-material";
import { FC } from "react";
import { Form, FormikProvider, useFormik } from "formik";

import { useActions } from "../../../hooks/useActions";

import { toLowerFirstLetter } from "../../../http_comon";
import { ServerError } from "../../../store/types";

import { IResetPasswordEmail } from "./types";
import { ResetPasswordEmailSchema } from "./validation";
import { TextFieldFirstStyle } from "../../../components/TextField/style";
import { LoadingButtonStyle } from "../../../components/LoadingButton/styled";

interface Props {
    dialogClose: any,
    changeDialog: any
}

const EmailDialog: FC<Props> = ({ dialogClose, changeDialog }) => {
    const { SendResetPasswordByEmail } = useActions();
    const resetPasswordModel: IResetPasswordEmail = { email: '' };

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
            <DialogTitle sx={{ py: "36px" }}>
                <Typography fontSize="30px" align="center" lineHeight="38px">
                    Reset password
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
                                    label="Email address"
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
                            Reset
                        </LoadingButtonStyle>
                    </DialogActions>
                    <Typography variant='subtitle1' align="center" sx={{ cursor: "pointer", mb: "36px" }}
                        onClick={changeDialog}>
                        Reset password by phone number
                    </Typography>
                </Form>
            </FormikProvider>
        </>
    )
}

export default EmailDialog;