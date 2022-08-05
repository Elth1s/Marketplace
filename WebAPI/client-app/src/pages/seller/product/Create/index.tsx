import { LoadingButton } from "@mui/lab";
import { Box, Grid, Typography } from "@mui/material";
import { Form, FormikProvider, useFormik } from "formik";
import { FC, useEffect, useState } from "react"
import { useDropzone } from "react-dropzone";
import { useNavigate } from "react-router-dom";
import { upload_cloud } from "../../../../assets/icons";
import AutocompleteComponent from "../../../../components/Autocomplete";
import { TextFieldFirstStyle } from "../../../../components/TextField/styled";
import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";
import { toLowerFirstLetter } from "../../../../http_comon";
import { ServerError } from "../../../../store/types";
import { IProductCreate, IProductImage } from "../types";

interface Props {

}

const ProductCreate: FC<Props> = ({ }) => {
    const { GetCategoriesWithoutChildren, GetProductStatusesSeller, CreateProductImage } = useActions();

    const { categories, productStatuses } = useTypedSelector((store) => store.productSeller);

    const [imagesTest, setImagesTest] = useState<Array<IProductImage>>([]);
    const [test, setTest] = useState<Array<number>>([1, 2, 3, 4, 5]);

    const item: IProductCreate = {
        name: "",
        description: "",
        price: 0,
        count: 0,
        statusId: 0,
        categoryId: 0,
        images: []
    }

    const navigate = useNavigate();

    useEffect(() => {
        document.title = "Product create";
        getData();
    }, []);

    useEffect(() => {
        console.log(imagesTest)
    }, [imagesTest]);

    useEffect(() => {
        console.log(test)
    }, [test]);

    const getData = async () => {
        try {
            await GetCategoriesWithoutChildren();
            await GetProductStatusesSeller();
        }
        catch (ex) {
        }
    }

    const formik = useFormik({
        initialValues: item,
        // validationSchema: validationFields,
        enableReinitialize: true,
        onSubmit: async (values, { setFieldError }) => {
            try {
                console.log(values)
                // await CreateCategory(values);
                // navigate("/seller/product");
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

    const { getRootProps, getInputProps } = useDropzone({
        // Note how this callback is never invoked if drop occurs on the inner dropzone
        onDrop: (files) => {
            if (!files || files.length === 0) return;

            let tempList: Array<IProductImage> = [];
            let newTest = [6, 7, 8, 9, 10]
            files.forEach(element => {
                let reader = new FileReader();

                reader.readAsDataURL(element);

                reader.onload = async () => {
                    if (reader.result != null) {
                        let res = await CreateProductImage(reader.result as string) as unknown as IProductImage;
                        tempList.push(res);
                    }
                };
            });
            console.log(imagesTest)
            console.log(tempList)
            setImagesTest([...imagesTest, ...tempList]);
            console.log(test)
            console.log(newTest)
            setTest([...test, ...newTest]);
        }
    });

    const { errors, touched, isSubmitting, handleSubmit, setFieldValue, getFieldProps } = formik;

    return (
        <Box sx={{ flexGrow: 1, m: 1, mx: 3, }}>
            <Typography variant="h4" gutterBottom sx={{ my: "auto" }}>
                Create Product
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
                                    label="Name"
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
                                    label="Description"
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
                                    label="Price"
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
                                    label="Count"
                                    {...getFieldProps('count')}
                                    error={Boolean(touched.count && errors.count)}
                                    helperText={touched.count && errors.count}
                                />
                            </Grid>
                            <Grid item xs={6}>
                                <AutocompleteComponent
                                    label="Categoty"
                                    name="categoryId"
                                    error={errors.categoryId}
                                    touched={touched.categoryId}
                                    options={categories}
                                    getOptionLabel={(option) => option.name}
                                    isOptionEqualToValue={(option, value) => option?.id === value.id}
                                    defaultValue={undefined}
                                    onChange={(e, value) => { setFieldValue("categoryId", value?.id) }}
                                />
                            </Grid>
                            <Grid item xs={6}>
                                <AutocompleteComponent
                                    label="Product status"
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
                                            <Typography variant="subtitle1" align="center">
                                                Move photo or search to upload
                                            </Typography>
                                        </Box>
                                    </div>
                                </Box>
                                {imagesTest?.length != 0 &&
                                    imagesTest.map((row, index) => {
                                        return (
                                            <img
                                                key={`product_image_${index}`}
                                                src={row.name}
                                                alt="icon"
                                                style={{ width: "136px", height: "136px", borderRadius: "10px", border: "1px solid #F45626" }} />
                                        );
                                    })
                                }

                            </Grid>
                        </Grid>
                        <LoadingButton
                            sx={{ paddingX: "35px", mt: "30px" }}
                            size="large"
                            type="submit"
                            variant="contained"
                            loading={isSubmitting}
                        >
                            Create
                        </LoadingButton>
                    </Form>
                </FormikProvider>
            </Box >
        </Box >
    )
}

export default ProductCreate