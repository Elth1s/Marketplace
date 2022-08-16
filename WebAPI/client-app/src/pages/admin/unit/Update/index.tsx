import {
    Grid,
} from "@mui/material";
import { Edit } from "@mui/icons-material";

import { useState, FC } from "react";
import { useFormik } from "formik";
import { ServerError, UpdateProps } from "../../../../store/types";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";
import { useActions } from "../../../../hooks/useActions";
import { UnitSchema } from "../validation";
import TextFieldComponent from "../../../../components/TextField";
import DialogComponent from "../../../../components/Dialog";
import { toLowerFirstLetter } from "../../../../http_comon";


const Update: FC<UpdateProps> = ({ id, afterUpdate }) => {
    const [open, setOpen] = useState(false);

    const { GetUnitById, UpdateUnit } = useActions();
    const { selectedUnit } = useTypedSelector((store) => store.unit);

    const handleClickOpen = async () => {
        setOpen(true);
        await GetUnitById(id);
    };

    const handleClickClose = () => {
        setOpen(false);
    };

    const formik = useFormik({
        initialValues: selectedUnit,
        validationSchema: UnitSchema,
        enableReinitialize: true,
        onSubmit: async (values, { setFieldError }) => {
            try {
                await UpdateUnit(id, values);
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

            dialogTitle="Update unit"
            dialogBtnConfirm="Update"

            dialogContent={
                <Grid container rowSpacing={2}>
                    <Grid item xs={12}>
                        <TextFieldComponent
                            type="text"
                            label="English Measure"
                            error={errors.englishMeasure}
                            touched={touched.englishMeasure}
                            getFieldProps={{ ...getFieldProps('englishMeasure') }}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextFieldComponent
                            type="text"
                            label="Ukrainian Measure"
                            error={errors.ukrainianMeasure}
                            touched={touched.ukrainianMeasure}
                            getFieldProps={{ ...getFieldProps('ukrainianMeasure') }}
                        />
                    </Grid>
                </Grid>
            }
        />
    )
}
export default Update;