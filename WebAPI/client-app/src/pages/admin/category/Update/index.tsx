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

const CategoryUpdate: FC<UpdateProps> = ({ id, afterUpdate }) => {
    const { t } = useTranslation();

    const { GetCategoryById, GetCategoryForSelect, UpdateCategory } = useActions();
    const { selectedCategory, categoriesForSelect } = useTypedSelector((store) => store.category);

    const [open, setOpen] = useState(false);

    const handleClickOpen = async () => {
        try {
            setOpen(true);
            await GetCategoryForSelect();
            await GetCategoryById(id);
        } catch (ex) {
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
        urlSlug: Yup.string().min(2).max(50).matches(urlSlugRegExp, 'Invalid format of  url slug').required().label(t('validationProps.urlSlug')),
        parentId: Yup.number().nullable().required().label(t('validationProps.categotyParent')),
    });

    const formik = useFormik({
        initialValues: selectedCategory,
        validationSchema: validationFields,
        enableReinitialize: true,
        onSubmit: async (values, { setFieldError }) => {
            try {
                await UpdateCategory(id, values);
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
            <Edit onClick={() => handleClickOpen()} />
            <AdminSellerDialog
                open={open}
                onClose={handleClickClose}
                dialogContent={
                    <>
                        <DialogTitleWithButton
                            title={t('pages.admin.category.updateTitle')}
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
                                            <AutocompleteComponent
                                                label={t('validationProps.categotyParent')}
                                                name="parentId"
                                                error={errors.parentId}
                                                touched={touched.parentId}
                                                options={categoriesForSelect}
                                                getOptionLabel={(option) => option.name}
                                                isOptionEqualToValue={(option, value) => option?.id === value.id}
                                                defaultValue={undefined}
                                                onChange={(e, value) => { setFieldValue("parentId", value?.id) }}
                                            />
                                        </Grid>
                                        <Grid item xs={3} sx={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "space-between" }}>
                                            <Typography variant="h4" color="inherit" align="center">
                                                {t("validationProps.categoryImage")}
                                            </Typography>
                                            <CropperDialog imgSrc={formik.values.image} onDialogSave={onSaveImage} isDark={true} />
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
                                            <CropperDialog imgSrc={formik.values.darkIcon} onDialogSave={onSaveDarkIcon} isDark={true} />
                                        </Grid>
                                        <Grid item xs={3} sx={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "space-between" }}>
                                            <Typography variant="h4" color="inherit" align="center">
                                                {t("validationProps.categoryActiveIcon")}
                                            </Typography>
                                            <CropperDialog imgSrc={formik.values.activeIcon} onDialogSave={onSaveActiveIcon} isDark={true} />
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

export default CategoryUpdate;