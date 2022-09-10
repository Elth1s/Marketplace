import { Box, DialogActions, DialogContent, DialogTitle, IconButton, Typography, Grid, Button, useTheme } from '@mui/material';
import { Close, StarRounded } from '@mui/icons-material';
import { LoadingButton } from '@mui/lab';

import { FC, useEffect, useState } from "react";
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';
import { toast } from 'react-toastify';

import { Form, FormikProvider, useFormik } from "formik";
import * as Yup from "yup"

import { IReply } from "../types";
import { ServerError } from '../../../../store/types';

import { ReviewQustionDialogStyle } from '../../../../components/Dialog/styled';
import { TextFieldSecondStyle } from '../../../../components/TextField/styled';
import { RatingStyle } from '../../../../components/Rating/styled';
import { ToastError, ToastWarning } from '../../../../components/ToastComponent';

import { useTypedSelector } from '../../../../hooks/useTypedSelector';
import { useActions } from "../../../../hooks/useActions";
import { reply, upload_cloud } from '../../../../assets/icons';
import CropperDialog from '../../../../components/CropperDialog';


interface Props {
    create: any
}

const AddReply: FC<Props> = ({ create }) => {
    const { t } = useTranslation();

    const { user } = useTypedSelector(state => state.auth)

    const [dialogOpen, setDialogOpen] = useState(false);

    const item: IReply = {
        fullName: "",
        email: user.isEmailExist ? user.emailOrPhone : "",
        text: "",
    };

    const handleClickOpen = () => {
        setDialogOpen(true);
    };

    const handleClickClose = () => {
        setDialogOpen(false);
    };

    const onHandleSubmit = async (values: IReply) => {
        try {
            create(values);
            handleClickClose();
        } catch (ex) {
            const serverErrors = ex as ServerError;
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
        }
    }

    const validationFields = Yup.object().shape({
        fullName: Yup.string().min(2).max(80).required().label(t('validationProps.fullName')),
        email: Yup.string().email().required().label(t('validationProps.email')),
        text: Yup.string().min(1).max(600).required().label(t('validationProps.reply')),
    });
    const formik = useFormik({
        initialValues: item,
        validationSchema: validationFields,
        onSubmit: onHandleSubmit
    });

    const { errors, touched, isSubmitting, handleSubmit, setFieldValue, setFieldError, getFieldProps, resetForm } = formik;

    return (
        <>
            <Button
                sx={{
                    color: "inherit",
                    textTransform: "none",
                    fontSize: "18px",
                    mr: "40px",
                    "&:hover": { background: "transparent" },
                    "&& .MuiTouchRipple-child": { backgroundColor: "transparent" }
                }}
                startIcon={
                    <img
                        style={{ width: "30px", height: "30px" }}
                        src={reply}
                        alt="replyIcon"
                    />
                }
                onClick={handleClickOpen}
            >
                {t("components.reviewItem.reply")}
            </Button>
            <ReviewQustionDialogStyle
                open={dialogOpen}
                onClose={handleClickClose}
                aria-describedby="alert-dialog-slide-description"
            >
                <DialogTitle sx={{ pt: "26px", pb: "20px", px: "30px" }}>
                    <Typography fontSize="30px">
                        {t("components.replyDialog.title")}
                    </Typography>
                    <IconButton
                        color="inherit"
                        onClick={handleClickClose}
                        sx={{
                            position: 'absolute',
                            right: 20,
                            top: 28,
                            borderRadius: "12px"
                        }}
                    >
                        <Close />
                    </IconButton>
                </DialogTitle>
                <FormikProvider value={formik} >
                    <Form autoComplete="off" noValidate onSubmit={handleSubmit} >
                        <DialogContent sx={{ width: "690px", mx: "auto", p: 0 }}>
                            <Grid container rowSpacing="20px" columnSpacing="30px">
                                <Grid item xs={12}>
                                    <TextFieldSecondStyle
                                        fullWidth
                                        variant="outlined"
                                        type="text"
                                        placeholder={t('validationProps.fullName')}
                                        {...getFieldProps('fullName')}
                                        error={Boolean(touched.fullName && errors.fullName)}
                                        helperText={touched.fullName && errors.fullName}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <TextFieldSecondStyle
                                        disabled={user.isEmailExist}
                                        fullWidth
                                        variant="outlined"
                                        type="text"
                                        placeholder={t('validationProps.email')}
                                        autoComplete="email"
                                        {...getFieldProps('email')}
                                        error={Boolean(touched.email && errors.email)}
                                        helperText={touched.email && errors.email}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <TextFieldSecondStyle
                                        fullWidth
                                        multiline
                                        rows={3}
                                        variant="outlined"
                                        type="text"
                                        placeholder={t('validationProps.reply')}
                                        {...getFieldProps('text')}
                                        error={Boolean(touched.text && errors.text)}
                                        helperText={touched.text && errors.text}
                                    />
                                </Grid>
                            </Grid>
                        </DialogContent>
                        <DialogActions sx={{ pt: "25px", pb: "26px", px: "30px" }}>
                            <LoadingButton
                                color="primary"
                                variant="contained"
                                loading={isSubmitting}
                                type="submit"
                                sx={{ height: "55px", ml: "auto", fontSize: "20px", py: "15px", px: "71px", textTransform: "none" }}
                            >
                                {t("components.replyDialog.button")}
                            </LoadingButton>
                        </DialogActions>
                    </Form>
                </FormikProvider>
            </ReviewQustionDialogStyle>
        </>
    )
}

export default AddReply;