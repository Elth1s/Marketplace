import {
    Box,
    Grid,
    Stack,
    Typography,
    CircularProgress,
    TextField,
    Autocomplete
} from "@mui/material";

import { useEffect, useState } from "react";
import { useNavigate } from "react-router";

import { Form, FormikProvider, useFormik } from "formik";
import { LoadingButton } from "@mui/lab";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { validationFields } from "../validation";
import { ICategory } from "../types";

import CropperDialog from "../../../../components/CropperDialog";

const CategoryUpdate = () => {
    const { GetByIdCategory, GetCategoryForSelect, GetCharacteristics, UpdateCategory } = useActions();
    const [loading, setLoading] = useState<boolean>(false);

    const { categoryInfo, categoriesForSelect } = useTypedSelector((store) => store.category);

    const category: ICategory = {
        name: categoryInfo.name,
        image: categoryInfo.image,
        parentId:categoriesForSelect.find(c => c.name === categoryInfo.parentName)?.id! || null,
    }

    const navigator = useNavigate();

    useEffect(() => {
        getData();
    }, []);

    const getData = async () => {
        setLoading(true);
        try {
            document.title = "Category";

            const params = new URLSearchParams(window.location.search);
            let id = params.get("id");

            await GetByIdCategory(id);
            await GetCategoryForSelect();
            await GetCharacteristics();

            setLoading(false);
        } catch (ex) {
            setLoading(false);
        }
    }

    const onHandleSubmit = async (values: ICategory) => {
        try {
            await UpdateCategory(categoryInfo.id, values);
            navigator("/category");
        }
        catch (ex) {

        }
    }

    const formik = useFormik({
        initialValues: category,
        validationSchema: validationFields,
        enableReinitialize: true,
        onSubmit: onHandleSubmit
    });

    const onSave = async (base64: string) => {
        setFieldValue("image", base64)
    };

    const { errors, touched, isSubmitting, handleSubmit, getFieldProps, setFieldValue } = formik;

    return (
        <Box sx={{ flexGrow: 1, m: 1, mx: 3, }}>

            <Stack direction="row" justifyContent="space-between" alignItems="center" sx={{ py: 1 }}>
                <Typography variant="h4" gutterBottom sx={{ my: "auto" }}>
                    Category Update
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
                            <Stack direction="row">
                                <Grid container spacing={4} sx={{ width: "70%" }}>

                                    <Grid item xs={12}>
                                        <Autocomplete
                                            autoHighlight
                                            id="parent-categoty-select"
                                            options={categoriesForSelect}
                                            getOptionLabel={(option) => option.name}
                                            isOptionEqualToValue={(option, value) => option.id === value.id}
                                            defaultValue={categoriesForSelect.find(value => value.id === category.parentId)}
                                            onChange={(e, value) => { setFieldValue("parentId", value?.id || null) }}
                                            //{...getFieldProps('parentId')}
                                            renderInput={(params) => (
                                                <TextField
                                                    {...params}
                                                    id="parentId"
                                                    label="Parent categoty"
                                                    name="parentId"
                                                    error={Boolean(touched.parentId && errors.parentId)}
                                                    helperText={touched.parentId && errors.parentId}
                                                />
                                            )}
                                        />
                                    </Grid>

                                    <Grid item xs={12}>
                                        <TextField
                                            fullWidth
                                            autoComplete="name"
                                            type="text"
                                            label="Name"
                                            {...getFieldProps('name')}
                                            error={Boolean(touched.name && errors.name)}
                                            helperText={touched.name && errors.name}
                                        />
                                    </Grid>

                                    <Grid item xs={12} mt={3} display="flex" justifyContent="space-between" >
                                        <LoadingButton
                                            sx={{ paddingX: "35px" }}
                                            size="large"
                                            type="submit"
                                            variant="contained"
                                            loading={isSubmitting}
                                        >
                                            Update
                                        </LoadingButton>
                                    </Grid>
                                </Grid>

                                <Grid container sx={{ display: 'flex', justifyContent: 'center', width: "30%" }} >
                                    <CropperDialog
                                        imgSrc={(formik.values.image === null || formik.values.image === "") ? "https://www.phoca.cz/images/projects/phoca-download-r.png" : formik.values.image}
                                        onDialogSave={onSave}
                                    />
                                </Grid>
                            </Stack>

                        </Form>
                    </FormikProvider>
                </Box>
            )}
        </Box>
    )
}

export default CategoryUpdate;