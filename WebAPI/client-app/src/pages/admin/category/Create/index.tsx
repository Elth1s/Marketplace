import { Grid, Typography } from "@mui/material";

import { FC, useState } from "react";
import { useTranslation } from "react-i18next";

import { Form, FormikProvider, useFormik } from "formik";
import * as Yup from 'yup';

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";
import { toLowerFirstLetter } from "../../../../http_comon";

import { ICategory } from "../types";
import { CreateProps, ServerError } from "../../../../store/types";

import { AdminSellerDialogActionsStyle, AdminSellerDialogContentStyle } from "../../../../components/Dialog/styled";
import { AdminDialogButton } from "../../../../components/Button/style";
import AdminSellerDialog from "../../../../components/Dialog";
import { TextFieldFirstStyle } from "../../../../components/TextField/styled";
import IconButtonPlus from "../../../../components/Button/IconButtonPlus";
import DialogTitleWithButton from "../../../../components/Dialog/DialogTitleWithButton";
import CropperDialog from "../../../../components/CropperDialog";

const CategoryCreate: FC<CreateProps> = ({ afterCreate }) => {
    const { t } = useTranslation();

    const { GetCategoryForSelect, CreateCategory } = useActions();
    const [open, setOpen] = useState(false);

    const { categoriesForSelect } = useTypedSelector((store) => store.category);

    const item: ICategory = {
        id: 0,
        englishName: "",
        ukrainianName: "",
        urlSlug: "",
        image: "",
        lightIcon: "",
        darkIcon: "",
        activeIcon: "",
        parentId: null
    }

    const handleClickOpen = async () => {
        try {
            setOpen(true);
            await GetCategoryForSelect();
        }
        catch (ex) {
        }
    };

    const handleClickClose = () => {
        setOpen(false);
        resetForm();
    };

    const urlSlugRegExp = /^[a-z0-9]+(?:-[a-z0-9]+)*$/
    const validationFields = Yup.object().shape({
        englishName: Yup.string().min(2).max(50).required().label(t('validationProps.englishName')),
        ukrainianName: Yup.string().min(2).max(50).required().label(t('validationProps.englishName')),
        urlSlug: Yup.string().min(2).max(50).matches(urlSlugRegExp, t('validationMessages.urlSlugMatch')).required().label(t('validationProps.urlSlug')),
        parentId: Yup.number().nullable().required().label(t('validationProps.categotyParent')),
    });

    const formik = useFormik({
        initialValues: item,
        validationSchema: validationFields,
        enableReinitialize: true,
        onSubmit: async (values, { setFieldError }) => {
            try {
                await CreateCategory(values);
                afterCreate();
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

    const onSaveImage = async (base64: string) => {
        setFieldValue("image", base64)
    };
    const onSaveLightIcon = async (base64: string) => {
        setFieldValue("lightIcon", base64)
    };
    const onSaveDarkIcon = async (base64: string) => {
        setFieldValue("darkIcon", base64)
    };
    const onSaveActiveIcon = async (base64: string) => {
        setFieldValue("activeIcon", base64)
    };

    const { errors, touched, isSubmitting, handleSubmit, setFieldValue, getFieldProps, resetForm } = formik;

    return (
        <>
            <IconButtonPlus onClick={handleClickOpen} />
            <AdminSellerDialog
                open={open}
                onClose={handleClickClose}
                dialogContent={
                    <>
                        <DialogTitleWithButton
                            title={t('pages.admin.category.createTitle')}
                            onClick={handleClickClose}
                        />
                        <FormikProvider value={formik} >
                            <Form onSubmit={handleSubmit}>
                                <AdminSellerDialogContentStyle>
                                    <Grid container spacing={2}>
                                        <Grid item xs={6}>
                                            <TextFieldFirstStyle
                                                fullWidth
                                                variant="standard"
                                                type="text"
                                                label={t('validationProps.englishName')}
                                                {...getFieldProps('englishName')}
                                                error={Boolean(touched.englishName && errors.englishName)}
                                                helperText={touched.englishName && errors.englishName}
                                            />
                                        </Grid>
                                        <Grid item xs={6}>
                                            <TextFieldFirstStyle
                                                fullWidth
                                                variant="standard"
                                                type="text"
                                                label={t('validationProps.ukrainianName')}
                                                {...getFieldProps('ukrainianName')}
                                                error={Boolean(touched.ukrainianName && errors.ukrainianName)}
                                                helperText={touched.ukrainianName && errors.ukrainianName}
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <TextFieldFirstStyle
                                                fullWidth
                                                variant="standard"
                                                type="text"
                                                label={t('validationProps.urlSlug')}
                                                {...getFieldProps('urlSlug')}
                                                error={Boolean(touched.urlSlug && errors.urlSlug)}
                                                helperText={touched.urlSlug && errors.urlSlug}
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <TextFieldFirstStyle
                                                select
                                                fullWidth
                                                variant="standard"
                                                label={t('validationProps.categotyParent')}
                                                error={Boolean(touched.parentId && errors.parentId)}
                                                helperText={touched.parentId && errors.parentId}
                                                {...getFieldProps('parentId')}
                                            >
                                                {categoriesForSelect && categoriesForSelect.map((item) =>
                                                    <MenuItem key={item.id} value={item.id}>{item.name}</MenuItem>
                                                )}
                                            </TextFieldFirstStyle>

                                        </Grid>
                                        <Grid item xs={3} sx={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "space-between" }}>
                                            <Typography variant="h4" color="inherit" align="center">
                                                {t("validationProps.categoryImage")}
                                            </Typography>
                                            <CropperDialog imgSrc={formik.values.image} onDialogSave={onSaveImage} />
                                        </Grid>
                                        <Grid item xs={3} sx={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "space-between" }}>
                                            <Typography variant="h4" color="inherit" align="center">
                                                {t("validationProps.categoryLightIcon")}
                                            </Typography>
                                            <CropperDialog imgSrc={formik.values.lightIcon} onDialogSave={onSaveLightIcon} isDark={true} />
                                        </Grid>
                                        <Grid item xs={3} sx={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "space-between" }}>
                                            <Typography variant="h4" color="inherit" align="center">
                                                {t("validationProps.categoryDarkIcon")}
                                            </Typography>
                                            <CropperDialog imgSrc={formik.values.darkIcon} onDialogSave={onSaveDarkIcon} />
                                        </Grid>
                                        <Grid item xs={3} sx={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "space-between" }}>
                                            <Typography variant="h4" color="inherit" align="center">
                                                {t("validationProps.categoryActiveIcon")}
                                            </Typography>
                                            <CropperDialog imgSrc={formik.values.activeIcon} onDialogSave={onSaveActiveIcon} />
                                        </Grid>
                                    </Grid>
                                </AdminSellerDialogContentStyle>
                                <AdminSellerDialogActionsStyle>
                                    <AdminDialogButton
                                        type="submit"
                                        variant="contained"
                                        color="primary"
                                        loading={isSubmitting}
                                    >
                                        {t('pages.admin.main.btnCreate')}
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

export default CategoryCreate;