import { LoadingButton } from "@mui/lab";
import { Box, Grid, Typography } from "@mui/material";
import { Form, FormikProvider, useFormik } from "formik";
import { FC, useEffect, useState } from "react"
import { useDropzone } from "react-dropzone";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import { upload_cloud } from "../../../../assets/icons";
import AutocompleteComponent from "../../../../components/Autocomplete";
import { TextFieldFirstStyle } from "../../../../components/TextField/styled";
import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";
import { toLowerFirstLetter } from "../../../../http_comon";
import { ServerError } from "../../../../store/types";
import { IProductRequest, IProductImage } from "../types";

const ProductCreate = () => {
    const { t } = useTranslation();

    const { GetCategoriesWithoutChildren, GetProductStatusesSeller, GetFiltersByCategoryId, CreateProductImage, CreateProduct, GetCharacteristicsByUser } = useActions();
    const { categories, productStatuses, filters, characteristics } = useTypedSelector((store) => store.productSeller);

    const navigate = useNavigate();

    const [imagesLoading, setImagesLoading] = useState<number>(0);



    const getData = async () => {
        try {
            await GetCategoriesWithoutChildren();
            await GetProductStatusesSeller();
            await GetCharacteristicsByUser();
        }
        catch (ex) {
        }
    }

    const item: IProductRequest = {
        name: "",
        description: "",
        price: 0,
        count: 0,
        statusId: 0,
        categoryId: 0,
        images: [],
        filtersValue: [],
        characteristicsValue: []
    }

    const formik = useFormik({
        initialValues: item,
        // validationSchema: validationFields,
        enableReinitialize: true,
        onSubmit: async (values, { setFieldError }) => {
            try {
                console.log(values)
                await CreateProduct(values);
                navigate("/seller/products");
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
    useEffect(() => {
        document.title = `${t("pages.seller.product.createTitle")}`;
        getData();
    }, [imagesLoading, formik.values.images]);


    const selectFilterValue = (nameId: number, valueId: number, customValue?: number) => {
        const index = formik.values.filtersValue.map(object => object.nameId).indexOf(nameId);

        const tmpList = formik.values.filtersValue.slice();

        if (index === -1)
            tmpList.push({ nameId: nameId, valueId: valueId, customValue: null })
        else {
            tmpList.splice(index, 1);
            tmpList.push({ nameId: nameId, valueId: valueId, customValue: null })
        }

        setFieldValue("filtersValue", tmpList);
    };

    const selectCharacteristicValue = (nameId: number, valueId: number) => {
        const index = formik.values.characteristicsValue.map(object => object.nameId).indexOf(nameId);

        const tmpList = formik.values.characteristicsValue.slice();

        if (index === -1)
            tmpList.push({ nameId: nameId, valueId: valueId })
        else {
            tmpList.splice(index, 1);
            tmpList.push({ nameId: nameId, valueId: valueId })
        }

        setFieldValue("characteristicsValue", tmpList);
    };

    const { getRootProps, getInputProps } = useDropzone({
        onDrop: (files) => {
            if (!files || files.length === 0) return;

            setImagesLoading(files.length)
            let tmp = formik.values.images.slice();
            files.forEach(element => {
                let reader = new FileReader();

                reader.readAsDataURL(element);
                reader.onload = async () => {
                    if (reader.result != null) {
                        let res = await CreateProductImage(reader.result as string) as unknown as IProductImage;
                        tmp.push(res);
                    }
                };
            });

            setFieldValue("images", tmp)
            setImagesLoading(0)
        }
    });

    const { errors, touched, isSubmitting, handleSubmit, setFieldValue, getFieldProps } = formik;

    return (
        <Box sx={{ flexGrow: 1, m: 1, mx: 3, }}>
            <Typography variant="h4" color="inherit" gutterBottom sx={{ my: "auto" }}>
                {t("pages.seller.product.createTitle")}
            </Typography>

            <Box sx={{ mt: 3 }} >
                <FormikProvider value={formik} >
                    <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
                        <Grid container spacing={2}>
                            <Grid item xs={12}>
                                <TextFieldFirstStyle
                                    fullWidth
                                    variant="standard"
                                    autoComplete="name"
                                    type="text"
                                    label={t('validationProps.name')}
                                    {...getFieldProps('name')}
                                    error={Boolean(touched.name && errors.name)}
                                    helperText={touched.name && errors.name}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextFieldFirstStyle
                                    fullWidth
                                    multiline
                                    rows={4}
                                    variant="standard"
                                    autoComplete="description"
                                    type="text"
                                    label={t('validationProps.description')}
                                    {...getFieldProps('description')}
                                    error={Boolean(touched.description && errors.description)}
                                    helperText={touched.description && errors.description}
                                />
                            </Grid>
                            <Grid item xs={6}>
                                <TextFieldFirstStyle
                                    fullWidth
                                    variant="standard"
                                    autoComplete="price"
                                    type="text"
                                    label={t('validationProps.price')}
                                    {...getFieldProps('price')}
                                    error={Boolean(touched.price && errors.price)}
                                    helperText={touched.price && errors.price}
                                />
                            </Grid>
                            <Grid item xs={6}>
                                <TextFieldFirstStyle
                                    fullWidth
                                    variant="standard"
                                    autoComplete="count"
                                    type="text"
                                    label={t('validationProps.count')}
                                    {...getFieldProps('count')}
                                    error={Boolean(touched.count && errors.count)}
                                    helperText={touched.count && errors.count}
                                />
                            </Grid>
                            <Grid item xs={6}>
                                <AutocompleteComponent
                                    label={t('validationProps.category')}
                                    name="categoryId"
                                    error={errors.categoryId}
                                    touched={touched.categoryId}
                                    options={categories}
                                    getOptionLabel={(option) => option.name}
                                    isOptionEqualToValue={(option, value) => option?.id === value.id}
                                    defaultValue={undefined}
                                    onChange={async (e, value) => {
                                        setFieldValue("categoryId", value?.id)
                                        await GetFiltersByCategoryId(value?.id);

                                    }}
                                />
                            </Grid>
                            <Grid item xs={6}>
                                <AutocompleteComponent
                                    label={t('validationProps.productStatus')}
                                    name="statusId"
                                    error={errors.statusId}
                                    touched={touched.statusId}
                                    options={productStatuses}
                                    getOptionLabel={(option) => option.name}
                                    isOptionEqualToValue={(option, value) => option?.id === value.id}
                                    defaultValue={undefined}
                                    onChange={(e, value) => { setFieldValue("statusId", value?.id) }}
                                />
                            </Grid>
                            <Grid item xs={6}>
                                {filters?.length != 0 &&
                                    <>
                                        <Typography variant="h2" color="secondary" align="center">
                                            {t("pages.seller.product.filters")}
                                        </Typography>
                                        {filters.map((filterGroup, index) => {
                                            return (
                                                <>
                                                    <Typography key={`filter_group_${index}`} variant="h2" color="inherit">
                                                        {filterGroup.name}
                                                    </Typography>
                                                    {filterGroup.filterNames.map((filterName, index) => {
                                                        return (
                                                            <Grid key={`filter_name_${index}`} container sx={{ display: "flex", alignItems: "center" }} >
                                                                <Grid item xs={6}>
                                                                    <Typography variant="h4" color="inherit">
                                                                        {filterName.name}
                                                                    </Typography>
                                                                </Grid>
                                                                <Grid item xs={6}>
                                                                    <AutocompleteComponent
                                                                        label={t('validationProps.filterValue')}
                                                                        name="value"
                                                                        // error={errors.statusId}
                                                                        // touched={touched.statusId}
                                                                        options={filterName.filterValues}
                                                                        getOptionLabel={(option) => option.value}
                                                                        isOptionEqualToValue={(option, value) => option?.id === value.id}
                                                                        defaultValue={undefined}
                                                                        onChange={(e, value) => { selectFilterValue(filterName.id, value?.id) }}
                                                                    />
                                                                </Grid>
                                                            </Grid>
                                                        );
                                                    })}
                                                </>
                                            );
                                        })}
                                    </>
                                }
                                {characteristics?.length != 0 &&
                                    <>
                                        <Typography variant="h2" color="secondary" align="center" sx={{ mt: "10px" }}>
                                            {t("pages.seller.product.characterictics")}
                                        </Typography>
                                        {characteristics.map((characteristicGroup, index) => {
                                            return (
                                                <>
                                                    <Typography key={`characteristic_group_${index}`} variant="h2" color="inherit">
                                                        {characteristicGroup.name}
                                                    </Typography>
                                                    {characteristicGroup.characteristicNames.map((characteristicName, index) => {
                                                        return (
                                                            <Grid key={`characteristic_name_${index}`} container sx={{ display: "flex", alignItems: "center" }}>
                                                                <Grid item xs={6}>
                                                                    <Typography variant="h4" color="inherit">
                                                                        {characteristicName.name}
                                                                    </Typography>
                                                                </Grid>
                                                                <Grid item xs={6}>
                                                                    <AutocompleteComponent
                                                                        label={t('validationProps.characteristicValue')}
                                                                        name="value"
                                                                        // error={errors.statusId}
                                                                        // touched={touched.statusId}
                                                                        options={characteristicName.characteristicValues}
                                                                        getOptionLabel={(option) => option.value}
                                                                        isOptionEqualToValue={(option, value) => option?.id === value.id}
                                                                        defaultValue={undefined}
                                                                        onChange={(e, value) => { selectCharacteristicValue(characteristicName.id, value?.id) }}
                                                                    />
                                                                </Grid>
                                                            </Grid>
                                                        );
                                                    })}
                                                </>
                                            );
                                        })}
                                    </>
                                }
                            </Grid>
                            <Grid item xs={6}>
                                <Box
                                    sx={{
                                        height: "150px",
                                        borderRadius: "10px",
                                        cursor: "pointer",
                                        border: "1px solid #F45626",
                                        display: "flex", flexDirection: "column", justifyContent: "center", alignItems: "center"
                                    }}
                                >
                                    <div {...getRootProps({ className: 'dropzone' })}>
                                        <input {...getInputProps()} />
                                        <Box sx={{ display: "flex", flexDirection: "column", alignItems: "center" }}>
                                            <img
                                                src={upload_cloud}
                                                alt="icon"
                                                style={{ width: "25px", height: "25px" }} />
                                            <Typography variant="subtitle1" color="inherit" align="center">
                                                {t('pages.user.createProduct.photoOrSearch')}
                                            </Typography>
                                        </Box>
                                    </div>
                                </Box>
                                <Box sx={{ display: "flex" }}>
                                    {imagesLoading > 0
                                        ? <div>{t('pages.user.createProduct.imagesUploaded')}</div>
                                        : (formik.values.images?.length != 0 &&
                                            formik.values.images.map((row, index) => {
                                                return (
                                                    <Box
                                                        sx={{
                                                            width: "136px",
                                                            height: "136px",
                                                            marginTop: "10px",
                                                            marginRight: "10px",
                                                            borderRadius: "10px",
                                                            border: "1px solid #F45626",
                                                        }}
                                                    >
                                                        <img
                                                            key={`product_image_${index}`}
                                                            src={row.name}
                                                            alt="icon"
                                                            style={{
                                                                width: "134px",
                                                                height: "134px",
                                                                borderRadius: "10px",
                                                                objectFit: "scale-down"
                                                            }}
                                                        />
                                                    </Box>
                                                );
                                            }))
                                    }
                                </Box>
                            </Grid>
                        </Grid>
                        <Grid item xs={12} sx={{ display: "flex", justifyContent: "end" }}>
                            <LoadingButton
                                sx={{ paddingX: "35px", mt: "30px" }}
                                size="large"
                                type="submit"
                                variant="contained"
                                loading={isSubmitting}
                            >
                                {t("pages.seller.main.btnCreate")}
                            </LoadingButton>
                        </Grid>
                    </Form>
                </FormikProvider>
            </Box >
        </Box >
    )
}

export default ProductCreate