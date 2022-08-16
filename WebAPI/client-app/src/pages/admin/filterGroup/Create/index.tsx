import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';

import { FC, useEffect, useState } from "react";
import { useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";

import { validationFields } from "../validation";
import { IFilterGroup } from "../types";
import { CreateProps, ServerError } from '../../../../store/types';

import DialogComponent from '../../../../components/Dialog';
import TextFieldComponent from "../../../../components/TextField";
import { toLowerFirstLetter } from '../../../../http_comon';

const FilterGroupCreate: FC<CreateProps> = ({ afterCreate }) => {
    const [open, setOpen] = useState(false);

    const { CreateFilterGroup } = useActions();

    const item: IFilterGroup = {
        englishName: "",
        ukrainianName: "",
    };

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClickClose = () => {
        setOpen(false);
    };

    const onHandleSubmit = async (values: IFilterGroup) => {
        try {
            await CreateFilterGroup(values);
            afterCreate();
            resetForm();
            handleClickClose();
        } catch (ex) {
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
                        my: 2,
                        px: 4,
                    }}
                    onClick={handleClickOpen}
                >
                    Create
                </Button>
            }

            formik={formik}
            isSubmitting={isSubmitting}
            handleSubmit={handleSubmit}

            dialogTitle="Create filter group"
            dialogBtnConfirm="Create"

            dialogContent={
                <Grid container spacing={2}>
                    <Grid item xs={12}>
                        <TextFieldComponent
                            type="text"
                            label="English Name"
                            error={errors.englishName}
                            touched={touched.englishName}
                            getFieldProps={{ ...getFieldProps('englishName') }}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextFieldComponent
                            type="text"
                            label="Ukrainian Name"
                            error={errors.ukrainianName}
                            touched={touched.ukrainianName}
                            getFieldProps={{ ...getFieldProps('ukrainianName') }}
                        />
                    </Grid>
                </Grid>
            }
        />
    )
}

export default FilterGroupCreate;