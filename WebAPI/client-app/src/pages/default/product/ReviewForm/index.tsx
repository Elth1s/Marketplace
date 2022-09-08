import Box from "@mui/material/Box";
import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import LoadingButton from "@mui/lab/LoadingButton";

import * as Yup from 'yup';
import { FC } from "react";
import { useParams } from "react-router-dom";
import { Form, FormikProvider, useFormik } from "formik";
import { toast } from 'react-toastify';
import { useTranslation } from "react-i18next";

import { green_upload_cloud } from "../../../../assets/icons";

import { ToastError, ToastWarning } from "../../../../components/ToastComponent";
import { TextFieldSecondStyle } from "../../../../components/TextField/styled";
import { RatingStyle } from "../../../../components/Rating/styled";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { ServerError } from "../../../../store/types";

import { IReview } from "../types";
import { StarRounded } from "@mui/icons-material";

interface Props {
    getData: any
}

const ReviewForm: FC<Props> = ({ getData }) => {
    const { t } = useTranslation();

    let { urlSlug } = useParams();

    const { AddReview } = useActions();
    const { user } = useTypedSelector(state => state.auth)

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

    const reviewValidationFields = Yup.object().shape({
        fullName: Yup.string().required().min(2).max(60).label(t('validationProps.fullName')),
        email: Yup.string().required().label(t('validationProps.email')),
        advantages: Yup.string().max(100).label(t('validationProps.advantages')),
        disadvantages: Yup.string().max(100).label(t('validationProps.disadvantages')),
        comment: Yup.string().required().min(1).max(850).label(t('validationProps.comment')),
    });

    const onHandleSubmit = async (values: IReview) => {
        try {
            if (!urlSlug) {
                toast(<ToastError title={t("toastTitles.error")} message={t("components.reviewDialog.exceptions.urlSlugError")} />);
                return;
            }

            if (values.productRating === 0) {
                toast(<ToastWarning title={t("toastTitles.warning")} message={t("components.reviewDialog.exceptions.productRatingWarning")} />);
                return;
            }

            values.productSlug = urlSlug;

            await AddReview(values);

            getData();
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

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, getFieldProps, resetForm, setFieldValue } = formik;

    return (
        <FormikProvider value={formik} >
            <Form autoComplete="off" noValidate onSubmit={handleSubmit} >
                <Box
                    sx={{
                        display: "flex",
                        alignItems: "center",
                        mb: "25px"
                    }}
                >
                    <Typography sx={{ mr: "18px" }}>{t('pages.product.reviewForm.generalImpression')}</Typography>
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
                <Grid container rowSpacing={2.5}>
                    <Grid container item xs={12} columnSpacing={3.25}>
                        <Grid item xs={6}>
                            <TextFieldSecondStyle
                                fullWidth
                                variant="outlined"
                                type="text"
                                placeholder={t("validationProps.fullName")}
                                {...getFieldProps('fullName')}
                                error={Boolean(touched.fullName && errors.fullName)}
                                helperText={touched.fullName && errors.fullName}
                            />
                        </Grid>
                        <Grid item xs={6}>
                            <TextFieldSecondStyle
                                disabled={user.isEmailExist}
                                fullWidth
                                variant="outlined"
                                type="text"
                                placeholder={t("validationProps.email")}
                                autoComplete="email"
                                {...getFieldProps('email')}
                                error={Boolean(touched.email && errors.email)}
                                helperText={touched.email && errors.email}
                            />
                        </Grid>
                    </Grid>
                    <Grid item xs={12}>
                        <TextFieldSecondStyle
                            fullWidth
                            variant="outlined"
                            type="text"
                            placeholder={t("validationProps.advantages")}
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
                            placeholder={t("validationProps.disadvantages")}
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
                            placeholder={t("validationProps.comment")}
                            multiline
                            rows={5}
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
                            placeholder={t("validationProps.videoURL")}
                            {...getFieldProps('videoURL')}
                            error={Boolean(touched.videoURL && errors.videoURL)}
                            helperText={touched.videoURL && errors.videoURL}
                        />
                    </Grid>
                    <Grid item xs={12} sx={{ paddingTop: "24px" }}>
                        <Box
                            sx={{
                                display: "flex",
                                flexDirection: "column",
                                justifyContent: "center",
                                alignItems: "center",
                                width: "100px",
                                height: "100px",
                                borderRadius: "10px",
                                cursor: "pointer",
                                border: "1px solid #0E7C3A",
                            }}
                        >
                            {/* <div {...getRootProps({ className: 'dropzone' })}>
                                <input {...getInputProps()} /> */}
                            <Box
                                sx={{
                                    display: "flex",
                                    flexDirection: "column",
                                    alignItems: "center"
                                }}
                            >
                                <img
                                    src={green_upload_cloud}
                                    alt="icon"
                                    style={{ width: "25px", height: "25px" }}
                                />
                                <Typography variant="subtitle1" align="center">
                                    {t('pages.product.reviewForm.attachPhoto')}
                                </Typography>
                            </Box>
                        </Box>
                    </Grid>
                    <Grid item xs={12} sx={{ display: "flex", justifyContent: "flex-end", alignItems: "flex-end", mt: "25px" }}>
                        <LoadingButton
                            variant="contained"
                            color="secondary"
                            loading={isSubmitting}
                            type="submit"
                            sx={{
                                fontSize: "20px",
                                fontWeight: "500",
                                lineHeight: "25px",
                                padding: "15px 83px",
                                borderRadius: "10px"
                            }}
                        >
                            {t('pages.product.reviewForm.sendBtn')}
                        </LoadingButton>
                    </Grid>
                </Grid>
            </Form>
        </FormikProvider >
    )
}

export default ReviewForm;