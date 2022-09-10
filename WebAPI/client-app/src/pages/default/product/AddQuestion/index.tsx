import { Box, DialogActions, DialogContent, DialogTitle, IconButton, Typography, Grid, Button, useTheme } from '@mui/material';
import { Close, StarRounded } from '@mui/icons-material';
import { LoadingButton } from '@mui/lab';

import { FC, useEffect, useState } from "react";
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';
import { toast } from 'react-toastify';
import { Form, FormikProvider, useFormik } from "formik";

import { questionValidationFields } from "../validation";
import { IQuestion } from "../types";
import { ServerError } from '../../../../store/types';

import { ReviewQustionDialogStyle } from '../../../../components/Dialog/styled';
import { TextFieldSecondStyle } from '../../../../components/TextField/styled';
import { RatingStyle } from '../../../../components/Rating/styled';
import { ToastError, ToastWarning } from '../../../../components/ToastComponent';

import { useTypedSelector } from '../../../../hooks/useTypedSelector';
import { useActions } from "../../../../hooks/useActions";
import { upload_cloud } from '../../../../assets/icons';
import CropperDialog from '../../../../components/CropperDialog';


interface Props {
    getData: any
}

const AddQuestion: FC<Props> = ({ getData }) => {
    const { t } = useTranslation();
    const { palette } = useTheme();

    let { urlSlug } = useParams();

    const { AddQuestion } = useActions();
    const { user } = useTypedSelector(state => state.auth)

    const [dialogOpen, setDialogOpen] = useState(false);

    const item: IQuestion = {
        fullName: "",
        email: user.isEmailExist ? user.emailOrPhone : "",
        message: "",
        images: [],
        productSlug: ""
    };

    const handleClickOpen = () => {
        setDialogOpen(true);
    };

    const handleClickClose = () => {
        setDialogOpen(false);
    };

    const onHandleSubmit = async (values: IQuestion) => {
        try {
            if (!urlSlug) {
                toast(<ToastError title={t("toastTitles.error")} message={t("components.questionDialog.exceptions.urlSlugError")} />);
                return;
            }

            values.productSlug = urlSlug;

            await AddQuestion(values);
            getData();
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
    const formik = useFormik({
        initialValues: item,
        validationSchema: questionValidationFields,
        onSubmit: onHandleSubmit
    });

    const onSaveImage = (base64: string) => {
        var tempImages = formik.values.images.slice();

        tempImages.push(base64);
        setFieldValue("images", tempImages);
    }

    const { errors, touched, isSubmitting, handleSubmit, setFieldValue, setFieldError, getFieldProps, resetForm } = formik;

    return (
        <>
            <Button
                variant="contained"
                sx={{
                    fontSize: "24px",
                    lineHeight: "27px",
                    py: "13px",
                    px: "40px",
                    textTransform: "none",
                    borderRadius: "10px",
                    "&:hover": { background: palette.primary.main }
                }}
                onClick={handleClickOpen}
            >
                {t("pages.product.askQuestion")}
            </Button>
            <ReviewQustionDialogStyle
                open={dialogOpen}
                onClose={handleClickClose}
                aria-describedby="alert-dialog-slide-description"
            >
                <DialogTitle sx={{ pt: "26px", pb: "20px", px: "30px" }}>
                    <Typography fontSize="30px">
                        {t("components.questionDialog.title")}
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
                                        placeholder="Full name"
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
                                        placeholder="Email"
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
                                        placeholder="Message"
                                        {...getFieldProps('message')}
                                        error={Boolean(touched.message && errors.message)}
                                        helperText={touched.message && errors.message}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{ display: "flex" }}>
                                    {formik.values.images?.length != 0 &&
                                        formik.values.images.map((row, index) => {
                                            return (
                                                <img
                                                    key={`product_image_${index}`}
                                                    src={row}
                                                    alt="icon"
                                                    style={{ width: "100px", height: "100px", borderRadius: "10px", marginRight: "10px", marginBottom: "10px", border: "1px solid #F45626", objectFit: "contain" }} />
                                            );
                                        })
                                    }
                                    <CropperDialog imgSrc={""} onDialogSave={onSaveImage} />
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
                                {t("components.questionDialog.button")}
                            </LoadingButton>
                        </DialogActions>
                    </Form>
                </FormikProvider>
            </ReviewQustionDialogStyle>
        </>
    )
}

export default AddQuestion;