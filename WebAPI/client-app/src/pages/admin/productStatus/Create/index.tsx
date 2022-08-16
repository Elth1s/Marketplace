import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';

import { FC, useState } from "react";
import { useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";

import { fieldValidation } from "../validation";
import { IProductStatus } from "../types";
import { CreateProps, ServerError } from '../../../../store/types';

import DialogComponent from '../../../../components/Dialog';
import { toLowerFirstLetter } from '../../../../http_comon';
import { IconButton } from '@mui/material';
import { white_plus } from '../../../../assets/icons';
import { TextFieldFirstStyle } from '../../../../components/TextField/styled';

const ProductStatusCreate: FC<CreateProps> = ({ afterCreate }) => {
    const [open, setOpen] = useState(false);

    const { CreateProductStatus } = useActions();

    const item: IProductStatus = {
        englishName: "",
        ukrainianName: "",
    };

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClickClose = () => {
        setOpen(false);
    };

    const formik = useFormik({
        initialValues: item,
        validationSchema: fieldValidation,
        onSubmit: async (values, { setFieldError, resetForm }) => {
            try {
                await CreateProductStatus(values);
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
    });

    const { errors, touched, isSubmitting, handleSubmit, getFieldProps } = formik;

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

            dialogTitle="Create product status"
            dialogBtnConfirm="Create"

            dialogContent={
                <Grid container rowSpacing={2}>
                    <Grid item xs={12}>
                        <TextFieldFirstStyle
                            fullWidth
                            autoComplete="englishName"
                            variant="standard"
                            type="text"
                            label="English name"
                            {...getFieldProps('englishName')}
                            error={Boolean(touched.englishName && errors.englishName)}
                            helperText={touched.englishName && errors.englishName}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextFieldFirstStyle
                            fullWidth
                            autoComplete="ukrainianName"
                            variant="standard"
                            type="text"
                            label="Ukrainian name"
                            {...getFieldProps('ukrainianName')}
                            error={Boolean(touched.ukrainianName && errors.ukrainianName)}
                            helperText={touched.ukrainianName && errors.ukrainianName}
                        />
                    </Grid>
                </Grid>
            }
        />
    )
}

export default ProductStatusCreate;