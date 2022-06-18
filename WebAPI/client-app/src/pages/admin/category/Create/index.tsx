import Box from "@mui/material/Box";
import Grid from "@mui/material/Grid";
import Stack from "@mui/material/Stack";
import Typography from "@mui/material/Typography";
import CircularProgress from "@mui/material/CircularProgress";

import { useEffect, useState } from "react";
import { useNavigate } from "react-router";

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

const CategoryCreate = () => {
    const { GetCategoryForSelect, CreateCategory } = useActions();
    const [loading, setLoading] = useState<boolean>(false);

    const { categoriesForSelect } = useTypedSelector((store) => store.category);

    const item: ICategory = {
        name: "",
        image: "",
        parentId: null
    }

    const navigator = useNavigate();

    useEffect(() => {
        getData();
    }, []);

    const getData = async () => {
        setLoading(true);
        try {
            document.title = "Category create";

            await GetCategoryForSelect();

            setLoading(false);
        } catch (ex) {
            setLoading(false);
        }
    }

    const onHandleSubmit = async (values: ICategory) => {
        try {
            await CreateCategory(values);
            navigator("/category");
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
                        setFieldError(key.toLowerCase(), message);
                    }
                });
        }
    }

    const formik = useFormik({
        initialValues: item,
        validationSchema: validationFields,
        enableReinitialize: true,
        onSubmit: onHandleSubmit
    });

    const onSave = async (base64: string) => {
        setFieldValue("image", base64)
    };

    const { errors, touched, isSubmitting, handleSubmit, setFieldValue, setFieldError, getFieldProps } = formik;

    return (
        <Box sx={{ flexGrow: 1, m: 1, mx: 3, }}>

            <Stack direction="row" justifyContent="space-between" alignItems="center" sx={{ py: 1 }}>
                <Typography variant="h4" gutterBottom sx={{ my: "auto" }}>
                    Category Create
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
                                        <AutocompleteComponent
                                            label="Categoty parent"
                                            name="parentId"
                                            error={errors.name}
                                            touched={touched.name}
                                            options={categoriesForSelect}
                                            getOptionLabel={(option) => option.name}
                                            isOptionEqualToValue={(option, value) => option?.id === value.id}
                                            defaultValue={undefined}
                                            onChange={(e, value) => { setFieldValue("parentId", value?.id) }}
                                        />
                                    </Grid>

                                    <Grid item xs={12}>
                                        <TextFieldComponent
                                            type="text"
                                            label="Name"
                                            error={errors.name}
                                            touched={touched.name}
                                            getFieldProps={{ ...getFieldProps('name') }}
                                        />
                                    </Grid>
                                </Grid>

                                <Grid container item xs={2}>
                                    <Grid item xs={12}>
                                        <CropperDialog
                                            imgSrc={(formik.values.image === null || formik.values.image === "") ? "https://www.phoca.cz/images/projects/phoca-download-r.png" : formik.values.image}
                                            onDialogSave={onSave}
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