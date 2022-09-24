import Grid from "@mui/material/Grid";
import { Edit } from "@mui/icons-material";

import { FC, useState } from "react";
import { useParams } from "react-router-dom";
import { useTranslation } from "react-i18next";

import { Form, FormikProvider, useFormik } from "formik";
import * as Yup from 'yup';

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { ServerError, UpdateProps } from "../../../../store/types";

import AutocompleteComponent from "../../../../components/Autocomplete";
import { TextFieldFirstStyle } from "../../../../components/TextField/styled";

import { toLowerFirstLetter } from "../../../../http_comon";
import AdminSellerDialog from "../../../../components/Dialog";
import DialogTitleWithButton from "../../../../components/Dialog/DialogTitleWithButton";
import { AdminSellerDialogActionsStyle, AdminSellerDialogContentStyle } from "../../../../components/Dialog/styled";
import { AdminDialogButton } from "../../../../components/Button/style";
import { Typography } from "@mui/material";
import CropperDialog from "../../../../components/CropperDialog";
import DatePickerComponent from "../../../../components/DatePicker";

const SaleUpdate: FC<UpdateProps> = ({ id, afterUpdate }) => {
    const { t } = useTranslation();

    const { GetSaleById, UpdateSale } = useActions();
    const { selectedSale } = useTypedSelector((store) => store.sale);

    const [open, setOpen] = useState(false);

    const handleClickOpen = async () => {
        try {
            setOpen(true);
            await GetSaleById(id);
        } catch (ex) {
        }
    };

    const handleClickClose = () => {
        setOpen(false);
        resetForm();
    };

    const validationFields = Yup.object().shape({
        name: Yup.string().required().min(2).max(90).label(t('validationProps.name')),
        discountMin: Yup.number().min(1).max(99).label(t('validationProps.discountMin')),
        discountMax: Yup.number().min(1).max(99).label(t('validationProps.discountMax')),
    });

    const formik = useFormik({
        initialValues: selectedSale,
        // validationSchema: validationFields,
        enableReinitialize: true,
        onSubmit: async (values, { setFieldError }) => {
            try {
                await UpdateSale(id, values);
                afterUpdate();
                handleClickClose();
            }
            catch (ex) {
                const serverErrors = ex as ServerError;
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
            }
        }
    });

    const onSaveUkrainianVerticalImage = async (base64: string) => {
        setFieldValue("ukrainianVerticalImage", base64)
    };
    const onSaveUkrainianHorizontalImage = async (base64: string) => {
        setFieldValue("ukrainianHorizontalImage", base64)
    };
    const onSaveEnglishHorizontalImage = async (base64: string) => {
        setFieldValue("englishHorizontalImage", base64)
    };
    const onSaveEnglishVerticalImage = async (base64: string) => {
        setFieldValue("englishVerticalImage", base64)
    };

    const { errors, touched, isSubmitting, handleSubmit, setFieldValue, getFieldProps, resetForm } = formik;

    return (
        <>
            <Edit onClick={() => handleClickOpen()} />
            <AdminSellerDialog
                open={open}
                onClose={handleClickClose}
                dialogContent={
                    <>
                        <DialogTitleWithButton
                            title={t('pages.admin.sale.updateTitle')}
                            onClick={handleClickClose}
                        />
                        <FormikProvider value={formik} >
                            <Form onSubmit={handleSubmit}>
                                <AdminSellerDialogContentStyle>
                                    <Grid container spacing={2}>
                                        <Grid item xs={12}>
                                            <TextFieldFirstStyle
                                                fullWidth
                                                variant="standard"
                                                type="text"
                                                label={t('validationProps.name')}
                                                {...getFieldProps('name')}
                                                error={Boolean(touched.name && errors.name)}
                                                helperText={touched.name && errors.name}
                                            />
                                        </Grid>
                                        <Grid item xs={6}>
                                            <TextFieldFirstStyle
                                                fullWidth
                                                variant="standard"
                                                type="text"
                                                label={t('validationProps.discountMin')}
                                                {...getFieldProps('discountMin')}
                                                error={Boolean(touched.discountMin && errors.discountMin)}
                                                helperText={touched.discountMin && errors.discountMin}
                                            />
                                        </Grid>
                                        <Grid item xs={6}>
                                            <TextFieldFirstStyle
                                                fullWidth
                                                variant="standard"
                                                type="text"
                                                label={t('validationProps.discountMax')}
                                                {...getFieldProps('discountMax')}
                                                error={Boolean(touched.discountMax && errors.discountMax)}
                                                helperText={touched.discountMax && errors.discountMax}
                                            />
                                        </Grid>
                                        <Grid item xs={6}>
                                            <DatePickerComponent
                                                label={t('validationProps.dateStart')}
                                                {...getFieldProps('dateStart')}
                                                error={errors.dateStart}
                                                touched={touched.dateStart}
                                                onChange={(value) => { setFieldValue('dateStart', value) }}
                                            />
                                        </Grid>
                                        <Grid item xs={6}>
                                            <DatePickerComponent
                                                label={t('validationProps.dateEnd')}
                                                {...getFieldProps('dateEnd')}
                                                error={errors.dateEnd}
                                                touched={touched.dateEnd}
                                                onChange={(value) => { setFieldValue('dateEnd', value) }}
                                            />
                                        </Grid>
                                        <Grid item xs={3} sx={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "space-between" }}>
                                            <Typography variant="h4" color="inherit" align="center">
                                                {t("validationProps.ukrainianHorizontalImage")}
                                            </Typography>
                                            <CropperDialog imgSrc={formik.values.ukrainianHorizontalImage} onDialogSave={onSaveUkrainianHorizontalImage} isDark={true} />
                                        </Grid>
                                        <Grid item xs={3} sx={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "space-between" }}>
                                            <Typography variant="h4" color="inherit" align="center">
                                                {t("validationProps.ukrainianVerticalImage")}
                                            </Typography>
                                            <CropperDialog imgSrc={formik.values.ukrainianVerticalImage} onDialogSave={onSaveUkrainianVerticalImage} isDark={true} />
                                        </Grid>
                                        <Grid item xs={3} sx={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "space-between" }}>
                                            <Typography variant="h4" color="inherit" align="center">
                                                {t("validationProps.englishHorizontalImage")}
                                            </Typography>
                                            <CropperDialog imgSrc={formik.values.englishHorizontalImage} onDialogSave={onSaveEnglishHorizontalImage} isDark={true} />
                                        </Grid>
                                        <Grid item xs={3} sx={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "space-between" }}>
                                            <Typography variant="h4" color="inherit" align="center">
                                                {t("validationProps.englishVerticalImage")}
                                            </Typography>
                                            <CropperDialog imgSrc={formik.values.englishVerticalImage} onDialogSave={onSaveEnglishVerticalImage} isDark={true} />
                                        </Grid>
                                    </Grid>
                                </AdminSellerDialogContentStyle>
                                <AdminSellerDialogActionsStyle>
                                    <AdminDialogButton
                                        type="submit"
                                        variant="contained"
                                        color="primary"
                                        disabled={isSubmitting}
                                    >
                                        {t('pages.admin.main.btnUpdate')}
                                    </AdminDialogButton>
                                </AdminSellerDialogActionsStyle>
                            </Form>
                        </FormikProvider>
                    </>
                }
            />
        </>
    )
}

export default SaleUpdate;