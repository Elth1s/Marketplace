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
import CropperDialog from "../../../../components/CropperDialog";

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

    const onSaveImage = (base64: string) => {
        var tempImages = formik.values.images.slice();

        tempImages.push(base64);
        setFieldValue("images", tempImages);
    }

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
                    <Typography color="inherit" sx={{ mr: "18px" }}>{t('pages.product.reviewForm.generalImpression')}</Typography>
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
                <Grid container rowSpacing={2.5} columnSpacing={3.25}>
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
                        <CropperDialog imgSrc={""} onDialogSave={onSaveImage} isGreen={true} />
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