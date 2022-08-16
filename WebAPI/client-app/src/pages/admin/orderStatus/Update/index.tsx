import {
    Grid,
} from "@mui/material";
import { Edit } from "@mui/icons-material";

import { useState, FC } from "react";
import { useFormik } from "formik";
import { ServerError, UpdateProps } from "../../../../store/types";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";
import { useActions } from "../../../../hooks/useActions";
import { fieldValidation } from "../validation";
import TextFieldComponent from "../../../../components/TextField";
import DialogComponent from "../../../../components/Dialog";
import { toLowerFirstLetter } from "../../../../http_comon";


const OrderStatusUpdate: FC<UpdateProps> = ({ id, afterUpdate }) => {
    const [open, setOpen] = useState(false);

    const { GetByIdOrderStatus, UpdateOrderStatus } = useActions();
    const { selectedOrderStatus } = useTypedSelector((store) => store.orderStatus);

    const handleClickOpen = async () => {
        setOpen(true);
        await GetByIdOrderStatus(id);
    };

    const handleClickClose = () => {
        setOpen(false);
    };

    const formik = useFormik({
        initialValues: selectedOrderStatus,
        validationSchema: fieldValidation,
        enableReinitialize: true,
        onSubmit: async (values, { setFieldError }) => {
            try {
                await UpdateOrderStatus(id, values);
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
                <Edit onClick={() => handleClickOpen()} />
            }

            formik={formik}
            isSubmitting={isSubmitting}
            handleSubmit={handleSubmit}

            dialogTitle="Update order status"
            dialogBtnConfirm="Update"

            dialogContent={
                <Grid container rowSpacing={2}>
                    <Grid item xs={12} >
                        <TextFieldComponent
                            type="text"
                            label="English Name"
                            error={errors.englishName}
                            touched={touched.englishName}
                            getFieldProps={{ ...getFieldProps('englishName') }}
                        />
                    </Grid>
                    <Grid item xs={12} >
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
export default OrderStatusUpdate;