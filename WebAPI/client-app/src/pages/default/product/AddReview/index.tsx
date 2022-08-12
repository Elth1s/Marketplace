import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';

import { FC, useState } from "react";
import { useTranslation } from 'react-i18next';
import { Form, FormikProvider, useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";

import { reviewValidationFields } from "../validation";
import { IReview } from "../types";
import { ServerError } from '../../../../store/types';

import { ReviewDialogStyle } from '../../../../components/Dialog/styled';
import { Box, DialogActions, DialogContent, DialogTitle, IconButton, Typography } from '@mui/material';
import { Close, StarRounded } from '@mui/icons-material';
import { LoadingButton } from '@mui/lab';
import { TextFieldSecondStyle } from '../../../../components/TextField/styled';
import { RatingStyle } from '../../../../components/Rating/styled';
import { useParams } from 'react-router-dom';
import { toast } from 'react-toastify';
import { ToastError, ToastWarning } from '../../../../components/ToastComponent';
import { useTypedSelector } from '../../../../hooks/useTypedSelector';


interface Props {
    getData: any
}

const AddReview: FC<Props> = ({ getData }) => {
    const { t } = useTranslation();

    let { urlSlug } = useParams();

    const { AddReview } = useActions();
    const { user } = useTypedSelector(state => state.auth)

    const [dialogOpen, setDialogOpen] = useState(false);

    const item: IReview = {
        productRating: 0,
        fullName: "",
        email: user.isEmailExist ? user.emailOrPhone : "",
        advantages: "",
        disadvantages: "",
        comment: "",
        images: [],
        videoURL: "",
        productSlug: ""
    };

    const handleClickOpen = () => {
        setDialogOpen(true);
    };

    const handleClickClose = () => {
        setDialogOpen(false);
    };

    const onHandleSubmit = async (values: IReview) => {
        try {
            if (!urlSlug) {
                toast(<ToastError title={t("toastTitles.error")} message={t("components.reviewDialog.exceptions.urlSlugError")} />);
                return;
            }

            if (values.productRating == 0) {
                toast(<ToastWarning title={t("toastTitles.warning")} message={t("components.reviewDialog.exceptions.productRatingWarning")} />);
                return;
            }
            values.productSlug = urlSlug;

            await AddReview(values);
            getData();
            handleClickClose();
            resetForm();
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
        validationSchema: reviewValidationFields,
        onSubmit: onHandleSubmit
    });

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
                    borderRadius: "10px"
                }}
                onClick={handleClickOpen}
            >
                {t("pages.product.addReview")}
            </Button>
            <ReviewDialogStyle
                open={dialogOpen}
                onClose={handleClickClose}
                aria-describedby="alert-dialog-slide-description"
            >
                <DialogTitle sx={{ py: "36px", pl: "32px" }}>
                    <Typography fontSize="35px" lineHeight="38px">
                        {t("components.reviewDialog.title")}
                    </Typography>
                    <IconButton
                        color="inherit"
                        onClick={handleClickClose}
                        sx={{
                            position: 'absolute',
                            right: 20,
                            top: 34,
                            borderRadius: "12px"
                        }}
                    >
                        <Close />
                    </IconButton>
                </DialogTitle>
                <FormikProvider value={formik} >
                    <Form autoComplete="off" noValidate onSubmit={handleSubmit} >
                        <DialogContent sx={{ width: "660px", mx: "auto", p: 0 }}>
                            <Box sx={{ display: "flex", mb: "30px" }}>
                                <Typography variant="h4" sx={{ pr: "18px" }}>
                                    {t("components.reviewDialog.overview")}
                                </Typography>
                                <RatingStyle
                                    sx={{ fontSize: "30px" }}
                                    value={formik.values.productRating}
                                    precision={0.5}
                                    icon={<StarRounded sx={{ fontSize: "30px" }} />}
                                    emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                                    onChange={(event, newValue: number | null) => {
                                        if (newValue != null)
                                            setFieldValue("productRating", newValue);
                                    }}

                                />
                            </Box>
                            <Grid container spacing="25px">
                                <Grid item xs={6}>
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
                                <Grid item xs={6} sx={{ pb: "15px" }}>
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
                                        placeholder="Comment"
                                        {...getFieldProps('comment')}
                                        error={Boolean(touched.comment && errors.comment)}
                                        helperText={touched.comment && errors.comment}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <TextFieldSecondStyle
                                        fullWidth
                                        variant="outlined"
                                        type="text"
                                        placeholder="Advantages"
                                        {...getFieldProps('advantages')}
                                        error={Boolean(touched.advantages && errors.advantages)}
                                        helperText={touched.advantages && errors.advantages}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <TextFieldSecondStyle
                                        fullWidth
                                        variant="outlined"
                                        type="text"
                                        placeholder="Disadvantages"
                                        {...getFieldProps('disadvantages')}
                                        error={Boolean(touched.disadvantages && errors.disadvantages)}
                                        helperText={touched.disadvantages && errors.disadvantages}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <TextFieldSecondStyle
                                        fullWidth
                                        variant="outlined"
                                        type="text"
                                        placeholder="Video URL"
                                        {...getFieldProps('videoURL')}
                                        error={Boolean(touched.videoURL && errors.videoURL)}
                                        helperText={touched.videoURL && errors.videoURL}
                                    />
                                </Grid>
                            </Grid>
                        </DialogContent>
                        <DialogActions sx={{ pt: "65px", pb: "26px", px: "32px" }}>
                            <LoadingButton
                                color="secondary"
                                variant="contained"
                                loading={isSubmitting}
                                type="submit"
                                sx={{ ml: "auto", fontSize: "20px", py: "15px", px: "80px", textTransform: "none" }}
                            >
                                {t("components.reviewDialog.button")}
                            </LoadingButton>
                        </DialogActions>
                    </Form>
                </FormikProvider>
            </ReviewDialogStyle>
        </>
    )
}

export default AddReview;