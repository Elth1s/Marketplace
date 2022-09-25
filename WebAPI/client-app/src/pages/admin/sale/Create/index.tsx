import { Box, Grid, IconButton, Paper, Typography } from "@mui/material";
import { Add, Remove } from "@mui/icons-material"

import { FC, useState } from "react";
import { useTranslation } from "react-i18next";

import { Form, FormikProvider, useFormik } from "formik";
import * as Yup from 'yup';

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";
import { toLowerFirstLetter } from "../../../../http_comon";

import { ISale } from "../types";
import { CreateProps, ServerError } from "../../../../store/types";

import { AdminSellerDialogActionsStyle, AdminSellerDialogContentStyle } from "../../../../components/Dialog/styled";
import { AdminDialogButton } from "../../../../components/Button/style";
import AdminSellerDialog from "../../../../components/Dialog";
import { TextFieldFirstStyle, TextFieldSecondStyle } from "../../../../components/TextField/styled";
import AutocompleteComponent from "../../../../components/Autocomplete";
import IconButtonPlus from "../../../../components/Button/IconButtonPlus";
import DialogTitleWithButton from "../../../../components/Dialog/DialogTitleWithButton";
import CropperDialog from "../../../../components/CropperDialog";
import DatePickerComponent from "../../../../components/DatePicker";
import { ICategory } from "../../../seller/product/types";

const SaleCreate: FC<CreateProps> = ({ afterCreate }) => {
    const { t } = useTranslation();

    const { CreateSale, GetCategoriesWithoutChildren } = useActions();
    const { categories } = useTypedSelector(state => state.productSeller);

    const [selectedCategories, setSelectedCategories] = useState<Array<ICategory>>([]);

    const [categorySearch, setCategorySearch] = useState<string>("");
    const [selectedCategorySearch, setSelectedCategorySearch] = useState<string>("");

    const [open, setOpen] = useState(false);

    const item: ISale = {
        name: "",
        ukrainianVerticalImage: "",
        ukrainianHorizontalImage: "",
        englishHorizontalImage: "",
        englishVerticalImage: "",
        discountMin: 0,
        discountMax: 0,
        dateStart: "",
        dateEnd: "",
        categories: []
    }

    const handleClickOpen = async () => {
        try {
            setOpen(true);
            await GetCategoriesWithoutChildren();
        }
        catch (ex) {
        }
    };

    const handleClickClose = () => {
        setOpen(false);
        setSelectedCategories([])
        resetForm();
    };

    const addSelectedCategory = (category: ICategory) => {
        let tmpList = selectedCategories.slice();

        let isExist = tmpList.find(tl => tl.id == category.id);

        if (!isExist) {
            tmpList.push(category);

            setSelectedCategories(tmpList);
        }
    }

    const removeSelectedCategory = (category: ICategory) => {
        let tmpList = selectedCategories.slice();

        let isExist = tmpList.find(tl => tl.id == category.id);

        if (isExist) {
            let index = selectedCategories.indexOf(isExist);
            tmpList.splice(index, 1);

            setSelectedCategories(tmpList);
        }
    }

    const validationFields = Yup.object().shape({
        name: Yup.string().required().min(2).max(90).label(t('validationProps.name')),
        discountMin: Yup.number().min(1).max(99).label(t('validationProps.discountMin')),
        discountMax: Yup.number().min(1).max(99).label(t('validationProps.discountMax')),
    });

    const formik = useFormik({
        initialValues: item,
        validationSchema: validationFields,
        enableReinitialize: true,
        onSubmit: async (values, { setFieldError }) => {
            try {
                let categoryIds = selectedCategories.map(item => item.id)
                values.categories = categoryIds;

                await CreateSale(values);
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
            <IconButtonPlus onClick={handleClickOpen} />
            <AdminSellerDialog
                open={open}
                onClose={handleClickClose}
                dialogContent={
                    <>
                        <DialogTitleWithButton
                            title={t('pages.admin.sale.createTitle')}
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
                                            <CropperDialog imgSrc={formik.values.ukrainianHorizontalImage} onDialogSave={onSaveUkrainianHorizontalImage} />
                                        </Grid>
                                        <Grid item xs={3} sx={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "space-between" }}>
                                            <Typography variant="h4" color="inherit" align="center">
                                                {t("validationProps.ukrainianVerticalImage")}
                                            </Typography>
                                            <CropperDialog imgSrc={formik.values.ukrainianVerticalImage} onDialogSave={onSaveUkrainianVerticalImage} />
                                        </Grid>
                                        <Grid item xs={3} sx={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "space-between" }}>
                                            <Typography variant="h4" color="inherit" align="center">
                                                {t("validationProps.englishHorizontalImage")}
                                            </Typography>
                                            <CropperDialog imgSrc={formik.values.englishHorizontalImage} onDialogSave={onSaveEnglishHorizontalImage} />
                                        </Grid>
                                        <Grid item xs={3} sx={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "space-between" }}>
                                            <Typography variant="h4" color="inherit" align="center">
                                                {t("validationProps.englishVerticalImage")}
                                            </Typography>
                                            <CropperDialog imgSrc={formik.values.englishVerticalImage} onDialogSave={onSaveEnglishVerticalImage} />
                                        </Grid>
                                        <Grid item xs={6}>
                                            <TextFieldSecondStyle
                                                fullWidth
                                                sx={{
                                                    pt: "10px",
                                                    px: "15px",
                                                }}
                                                size="small"
                                                placeholder={t('containers.admin_seller.table.search')}
                                                onChange={(event => {
                                                    setCategorySearch(event.target.value);
                                                })}
                                            />
                                            <Paper elevation={0} sx={{ maxHeight: "240px", minHeight: "240px", overflow: 'auto', mt: "5px" }}>
                                                {categories.map((value, index) => {
                                                    const isExist = selectedCategories.find(sc => sc.id == value.id)
                                                    if (value.name.includes(categorySearch))
                                                        return (
                                                            <Box
                                                                key={`categories_${index}`}
                                                                sx={{
                                                                    display: "flex",
                                                                    justifyContent: "space-between",
                                                                    alignItems: "center",
                                                                    px: "15px",
                                                                    pt: "10px"
                                                                }}
                                                            >
                                                                <Typography variant="h4" color="inherit">
                                                                    {value.name}
                                                                </Typography>
                                                                <IconButton
                                                                    onClick={() => addSelectedCategory(value)}
                                                                    sx={{ borderRadius: "12px" }}
                                                                    color="secondary"
                                                                    disabled={isExist ? true : false}
                                                                >
                                                                    <Add />
                                                                </IconButton>
                                                            </Box>
                                                        )
                                                })}
                                            </Paper>
                                        </Grid>
                                        <Grid item xs={6}>
                                            <Paper sx={{ maxHeight: "240px", minHeight: "240px", overflow: 'auto', '&::-webkit-scrollbar': { display: "none" } }}>
                                                <TextFieldSecondStyle
                                                    fullWidth
                                                    sx={{
                                                        pt: "10px",
                                                        px: "15px"
                                                    }}
                                                    size="small"
                                                    placeholder={t('containers.admin_seller.table.search')}
                                                    onChange={(event => {
                                                        setSelectedCategorySearch(event.target.value);
                                                    })}
                                                />
                                                {selectedCategories.map((value, index) => {
                                                    if (value.name.includes(selectedCategorySearch))
                                                        return (
                                                            <Box
                                                                key={`selected_categories_${index}`}
                                                                sx={{
                                                                    display: "flex",
                                                                    justifyContent: "space-between",
                                                                    alignItems: "center",
                                                                    px: "15px",
                                                                    pt: "10px"
                                                                }}
                                                            >
                                                                <Typography variant="h4" color="inherit">
                                                                    {value.name}
                                                                </Typography>
                                                                <IconButton
                                                                    onClick={() => removeSelectedCategory(value)}
                                                                    sx={{ borderRadius: "12px" }}
                                                                    color="error"
                                                                >
                                                                    <Remove />
                                                                </IconButton>
                                                            </Box>
                                                        )
                                                })}
                                            </Paper>
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

export default SaleCreate;