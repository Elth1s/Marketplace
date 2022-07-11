import Rating from '@mui/material/Rating';
import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';

import { useState } from "react";
import { useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";

import { validationFields } from "../validation";
import { IReview } from "../types";
import { ServerError } from '../../../../store/types';

import DialogComponent from '../../../../components/Dialog';
import TextFieldComponent from "../../../../components/TextField";

const AddReview = () => {
    const [open, setOpen] = useState(false);

    const item: IReview = {
        name: "",
        email: "",
        rating: null,
        advantages: "",
        disadvantages: "",
        review: "",
    };

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClickClose = () => {
        setOpen(false);
        resetForm();
    };

    const onHandleSubmit = async (values: IReview) => {
        try {
            console.log("data:", values)
            handleClickClose();
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
        validationSchema: validationFields,
        onSubmit: onHandleSubmit
    });

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, getFieldProps, resetForm } = formik;

    return (
        <DialogComponent
            open={open}
            handleClickClose={handleClickClose}
            button={
                <Button
                    variant="contained"
                    sx={{
                        fontSize: "20px",
                        py: "20px",
                        px: "78px"
                    }}
                    onClick={handleClickOpen}
                >
                    Додати відгук
                </Button>
            }

            formik={formik}
            isSubmitting={isSubmitting}
            handleSubmit={handleSubmit}

            dialogTitle="Відправити відгук"
            dialogBtnConfirm="Відправити"

            dialogContent={
                <Grid container spacing={2}>
                    <Grid item xs={12}>
                        <Rating
                            {...getFieldProps('rating')}
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
                    <Grid item xs={12}>
                        <TextFieldComponent
                            type="text"
                            label="Email"
                            error={errors.email}
                            touched={touched.email}
                            getFieldProps={{ ...getFieldProps('email') }}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextFieldComponent
                            type="text"
                            label="Advantages"
                            error={errors.advantages}
                            touched={touched.advantages}
                            getFieldProps={{ ...getFieldProps('advantages') }}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextFieldComponent
                            type="text"
                            label="Disadvantages"
                            error={errors.disadvantages}
                            touched={touched.disadvantages}
                            getFieldProps={{ ...getFieldProps('disadvantages') }}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextFieldComponent
                            type="text"
                            label="Review"
                            error={errors.review}
                            touched={touched.review}
                            getFieldProps={{ ...getFieldProps('review') }}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextFieldComponent
                            type="text"
                            label="Review"
                            error={errors.review}
                            touched={touched.review}
                            getFieldProps={{ ...getFieldProps('review') }}
                        />
                    </Grid>
                </Grid >
            }
        />
    )
}

export default AddReview;