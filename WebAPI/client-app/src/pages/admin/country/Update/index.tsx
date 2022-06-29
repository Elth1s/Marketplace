import {
    Box,
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    Grid,
    IconButton,
    Paper,
    Slide,
    TableCell
} from "@mui/material";
import { Close, Edit } from "@mui/icons-material";

import { LegacyRef, forwardRef, useRef, useState, useEffect, FC } from "react";
import Cropper from "cropperjs";
import { Form, FormikProvider, useFormik } from "formik";
import { ServerError } from "../../../../store/types";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";
import { useActions } from "../../../../hooks/useActions";
import { countryValidation } from "../validation";
import TextFieldComponent from "../../../../components/TextField";
import { LoadingButton } from "@mui/lab";
import DialogComponent from "../../../../components/Dialog";


interface Props {
    id: number,
    afterUpdate: any
}

const EditCountryDialog: FC<Props> = ({ id, afterUpdate }) => {
    const [open, setOpen] = useState(false);

    const { GetByIdCountry, UpdateCountry } = useActions();
    const { countryInfo } = useTypedSelector((store) => store.country);

    const handleClickOpen = async () => {
        setOpen(true);
        await GetByIdCountry(id);
    };

    const handleClickClose = () => {
        setOpen(false);
    };

    const formik = useFormik({
        initialValues: countryInfo,
        validationSchema: countryValidation,
        enableReinitialize: true,
        onSubmit: async (values, { setFieldError }) => {
            try {
                await UpdateCountry(countryInfo.id, values);
                // await GetCountries();
                afterUpdate();
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
                            setFieldError(key.toLowerCase(), message);
                        }
                    });
            }
        }
    });

    const { errors, touched, isSubmitting, handleSubmit, getFieldProps, resetForm } = formik;

    return (
        <DialogComponent
            open={open}
            handleClickClose={handleClickClose}
            button={
                <Edit onClick={() => handleClickOpen()} />
            }

            formik={formik}
            isSubmitting={isSubmitting}
            handleSubmit={handleSubmit}

            dialogTitle="Update country"
            dialogBtnConfirm="Update"

            dialogContent={
                <Grid container rowSpacing={2}>
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
                            label="Code"
                            error={errors.code}
                            touched={touched.code}
                            getFieldProps={{ ...getFieldProps('code') }}
                        />
                    </Grid>
                </Grid>
            }
        />
    )
}
export default EditCountryDialog;