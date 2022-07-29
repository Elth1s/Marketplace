import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';

import { FC, useState } from "react";
import { useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { validationFields } from "../validation";
import { ICharacteristicName } from "../types";
import { CreateProps, ServerError } from '../../../../store/types';

import DialogComponent from '../../../../components/Dialog';
import TextFieldComponent from "../../../../components/TextField";
import { toLowerFirstLetter } from '../../../../http_comon';
import AutocompleteComponent from '../../../../components/Autocomplete';
import CropperDialog from '../../../../components/CropperDialog';

const CharacteristicCreate: FC<CreateProps> = ({ afterCreate }) => {
    const [open, setOpen] = useState(false);

    const { CreateCharacteristicName, GetCharacteristicGroups, GetUnits } = useActions();

    const { characteristicGroups } = useTypedSelector((store) => store.characteristicGroup);
    const { units } = useTypedSelector((store) => store.unit);

    const item: ICharacteristicName = {
        name: '',
        characteristicGroupId: 0,
        unitId: null,
    }

    const handleClickOpen = async () => {
        await GetCharacteristicGroups();
        await GetUnits();
        setOpen(true);
    };

    const handleClickClose = () => {
        setOpen(false);
    };

    const onHandleSubmit = async (values: ICharacteristicName) => {
        try {
            await CreateCharacteristicName(values);
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

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, getFieldProps, resetForm, setFieldValue } = formik;

    const onSaveImage = async (base64: string) => {
        // setFieldValue("image", base64)
    };

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

            dialogTitle="Create characteristic name"
            dialogBtnConfirm="Create"

            dialogContent={
                <Grid container spacing={2}>
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
                        <AutocompleteComponent
                            label="Characteristic group"
                            name="characteristicGroupId"
                            error={errors.characteristicGroupId}
                            touched={touched.characteristicGroupId}
                            options={characteristicGroups}
                            getOptionLabel={(option) => option.name}
                            isOptionEqualToValue={(option, value) => option?.id === value.id}
                            defaultValue={undefined}
                            onChange={(e, value) => { setFieldValue("characteristicGroupId", value?.id) }}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <AutocompleteComponent
                            label="Unit measure"
                            name="unitId"
                            error={errors.unitId}
                            touched={touched.unitId}
                            options={units}
                            getOptionLabel={(option) => option.measure}
                            isOptionEqualToValue={(option, value) => option?.id === value.id}
                            defaultValue={undefined}
                            onChange={(e, value) => { setFieldValue("unitId", value?.id) }}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <CropperDialog
                            imgSrc={"https://www.phoca.cz/images/projects/phoca-download-r.png"}
                            onDialogSave={onSaveImage}
                            labelId="Image"
                        />
                    </Grid>
                </Grid>
            }
        />
    )
}

export default CharacteristicCreate;