import { Close } from "@mui/icons-material";
import { Button, DialogActions, DialogContent, DialogTitle, Grid, IconButton, Typography } from "@mui/material";
import { Form, FormikProvider, useFormik } from "formik";
import { FC, useState } from "react";
import { useNavigate } from "react-router-dom";
import { LoadingButtonStyle } from "../../../components/LoadingButton/styled";
import { TextFieldFirstStyle } from "../../../components/TextField/styled";
import { useActions } from "../../../hooks/useActions";
import { ServerError } from "../../../store/types";
import { IResetPasswordInfo, IResetPasswordPhone } from "./types";
import { ResetPasswordPhoneSchema } from "./validation";

interface Props {
    dialogClose: any,
    changeDialog: any
}

const EmailDialog: FC<Props> = ({ dialogClose, changeDialog }) => {
    const { SendResetPasswordByPhoneCode, ValidateCodeForResetPasswordByPhone } = useActions();

    const navigate = useNavigate();

    const [isCodeSend, setIsCodeSend] = useState(false);
    const resetPasswordModel: IResetPasswordPhone = { phone: '', code: '' };

    const formik = useFormik({
        initialValues: resetPasswordModel,
        validationSchema: ResetPasswordPhoneSchema,
        onSubmit: async (values, { setFieldError }) => {
            try {
                let res = await ValidateCodeForResetPasswordByPhone(values) as unknown as IResetPasswordInfo;
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

    const onClickSendCodeBtn = async () => {
        try {
            // await SendResetPasswordByPhoneCode({ phone: formik.values.phone });
            setIsCodeSend(true);
        }
        catch (extection) {
        }
    };

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
                                    label="Phone number"
                                    {...getFieldProps('phone')}
                                    error={Boolean(touched.phone && errors.phone)}
                                    helperText={touched.phone && errors.phone}
                                />

                            </Grid>
                            {isCodeSend && <Grid item xs={12} >
                                <TextFieldFirstStyle
                                    fullWidth
                                    variant="standard"
                                    type="text"
                                    label="Code"
                                    {...getFieldProps('code')}
                                    error={Boolean(touched.code && errors.phone)}
                                    helperText={touched.phone && errors.phone}
                                />
                            </Grid>}
                        </Grid>
                    </DialogContent>
                    <DialogActions sx={{ pt: "30px", pb: "16px", px: "32px" }}>
                        {isCodeSend
                            ? <LoadingButtonStyle
                                fullWidth
                                color="secondary"
                                variant="contained"
                                loading={isSubmitting}
                                type="submit"
                                sx={{ width: "auto", fontSize: "20px", lineHeight: "25px", py: "15px", px: "100px", mx: "auto", textTransform: "none" }}
                            >
                                Reset
                            </LoadingButtonStyle>
                            : <Button
                                fullWidth
                                color="secondary"
                                variant="contained"
                                sx={{ width: "auto", fontSize: "20px", lineHeight: "25px", py: "15px", px: "100px", mx: "auto", textTransform: "none", borderRadius: "0px" }}
                                onClick={onClickSendCodeBtn}
                            >
                                Send code
                            </Button>
                        }
                    </DialogActions>
                    <Typography variant='subtitle1' align="center" sx={{ cursor: "pointer", mb: "36px" }}
                        onClick={changeDialog}>
                        Reset password by email
                    </Typography>
                </Form>
            </FormikProvider>
        </>
    )
}

export default EmailDialog;