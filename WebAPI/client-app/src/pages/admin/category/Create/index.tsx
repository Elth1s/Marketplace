import Box from "@mui/material/Box";
import Grid from "@mui/material/Grid";
import Stack from "@mui/material/Stack";
import Typography from "@mui/material/Typography";
import CircularProgress from "@mui/material/CircularProgress";

import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import { Form, FormikProvider, useFormik } from "formik";
import { LoadingButton } from "@mui/lab";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { validationFields } from "../validation";
import { ICategory } from "../types";
import { ServerError } from "../../../../store/types";

import CropperDialog from "../../../../components/CropperDialog";
import TextFieldComponent from "../../../../components/TextField";
import AutocompleteComponent from "../../../../components/Autocomplete";
import { toLowerFirstLetter } from "../../../../http_comon";

const CategoryCreate = () => {
    const { GetCategoryForSelect, CreateCategory } = useActions();
    const [loading, setLoading] = useState<boolean>(false);

    const { categoriesForSelect } = useTypedSelector((store) => store.category);

    const item: ICategory = {
        id: 0,
        name: "",
        urlSlug: "",
        image: "",
        icon: "",
        parentId: null
    }

    const navigate = useNavigate();

    useEffect(() => {
        document.title = "Category create";
        getData();
    }, []);

    const getData = async () => {
        setLoading(true);
        try {
            await GetCategoryForSelect();
            setLoading(false);
        }
        catch (ex) {
            setLoading(false);
        }
    }

    const formik = useFormik({
        initialValues: item,
        validationSchema: validationFields,
        enableReinitialize: true,
        onSubmit: async (values, { setFieldError }) => {
            try {
                await CreateCategory(values);
                navigate("/admin/category");
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
    const onSaveIcon = async (base64: string) => {
        setFieldValue("icon", base64)
    };

    const { errors, touched, isSubmitting, handleSubmit, setFieldValue, getFieldProps } = formik;

    return (
        <Box sx={{ flexGrow: 1, m: 1, mx: 3, }}>

            <Stack direction="row" justifyContent="space-between" alignItems="center" sx={{ py: 1 }}>
                <Typography variant="h4" gutterBottom sx={{ my: "auto" }}>
                    Create Category
                </Typography>
            </Stack>

            {loading ? (
                <Box sx={{ display: 'flex', justifyContent: 'center' }}>
                    <CircularProgress sx={{ color: "#66fcf1", mt: 3 }} />
                </Box>
            ) : (
                <Box sx={{ mt: 3 }} >
                    <FormikProvider value={formik} >
                        <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
                            <Grid container spacing={2}>
                                <Grid container item xs={10}>
                                    <Grid item xs={12}>
                                        <TextFieldComponent
                                            type="text"
                                            label="Name"
                                            error={errors.name}
                                            touched={touched.name}
                                            getFieldProps={{ ...getFieldProps('name') }}
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextFieldComponent
                                            type="text"
                                            label="Url slug"
                                            error={errors.urlSlug}
                                            touched={touched.urlSlug}
                                            getFieldProps={{ ...getFieldProps('urlSlug') }}
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <AutocompleteComponent
                                            label="Categoty parent"
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
                                </Grid>
                                <Grid container item xs={2} rowSpacing={2}>
                                    <Grid item xs={12}>
                                        <CropperDialog
                                            imgSrc={(formik.values.image === null || formik.values.image === "") ? "https://www.phoca.cz/images/projects/phoca-download-r.png" : formik.values.image}
                                            onDialogSave={onSaveImage}
                                            labelId="Image"
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <CropperDialog
                                            imgSrc={(formik.values.icon === null || formik.values.icon === "") ? "https://www.phoca.cz/images/projects/phoca-download-r.png" : formik.values.icon}
                                            onDialogSave={onSaveIcon}
                                            labelId="Icon"
                                        />
                                    </Grid>
                                </Grid>
                            </Grid>
                            <LoadingButton
                                sx={{ paddingX: "35px" }}
                                size="large"
                                type="submit"
                                variant="contained"
                                loading={isSubmitting}
                            >
                                Create
                            </LoadingButton>
                        </Form>
                    </FormikProvider>
                </Box>
            )}
        </Box>
    )
}

export default CategoryCreate;