import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';

import { FC, useEffect, useState } from "react";
import { useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";

import { validationFields } from "../validation";
import { IFilterGroup } from "../types";
import { CreateProps, ServerError } from '../../../../store/types';

import DialogComponent from '../../../../components/Dialog';
import { toLowerFirstLetter } from '../../../../http_comon';
import { IconButton } from '@mui/material';
import { white_plus } from '../../../../assets/icons';
import { TextFieldFirstStyle } from '../../../../components/TextField/styled';

const FilterGroupCreate: FC<CreateProps> = ({ afterCreate }) => {
    const [open, setOpen] = useState(false);

    const { CreateFilterGroup } = useActions();

    const item: IFilterGroup = {
        name: "",
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
                <IconButton
                    sx={{ borderRadius: '12px', background: "#F45626", "&:hover": { background: "#CB2525" }, "&& .MuiTouchRipple-child": { backgroundColor: "transparent" } }}
                    size="large"
                    color="inherit"
                    onClick={handleClickOpen}
                >
                    <img
                        style={{ width: "30px" }}
                        src={white_plus}
                        alt="icon"
                    />
                </IconButton>
            }

            formik={formik}
            isSubmitting={isSubmitting}
            handleSubmit={handleSubmit}

            dialogTitle="Create filter group"
            dialogBtnConfirm="Create"

            dialogContent={
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
                </Grid>
            }
        />
    )
}

export default FilterGroupCreate;