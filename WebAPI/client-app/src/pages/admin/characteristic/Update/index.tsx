import {
    Box,
    Grid,
    Stack,
    Typography,
    CircularProgress,
    TextField,
    FormControl,
    InputLabel,
    Select,
    MenuItem,
    Autocomplete
} from "@mui/material";
import { LoadingButton } from "@mui/lab";

import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { Form, FormikProvider, useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { validationFields } from "../validation";
import { ICharacteristic } from "../types";

const CharacteristicUpdate = () => {
    const { GetByIdCharacteristic, GetCharacteristicGroups, UpdateCharacteristic } = useActions();
    const [loading, setLoading] = useState<boolean>(false);

    const { characteristicInfo } = useTypedSelector((store) => store.characteristic);
    const { characteristicGroups } = useTypedSelector((store) => store.characteristicGroup);

    const characteristic: ICharacteristic = {
        name: characteristicInfo.name,
        characteristicGroupId: characteristicGroups.find(n => n.name === characteristicInfo.characteristicGroupName)?.id!
    }

    const navigator = useNavigate();

    useEffect(() => {
        getData();
    }, []);

    const getData = async () => {
        setLoading(true);
        try {
            document.title = "Characteristic";

            const params = new URLSearchParams(window.location.search);
            let id = params.get("id");

            await GetCharacteristicGroups();
            await GetByIdCharacteristic(id);


            setLoading(false);
        } catch (ex) {
            setLoading(false);
        }
    }

    const onHandleSubmit = async (values: ICharacteristic) => {
        try {
            await UpdateCharacteristic(characteristicInfo.id, values);
            navigator("/characteristic");
        }
        catch (ex) {

        }
    }

    const formik = useFormik({
        initialValues: characteristic,
        validationSchema: validationFields,
        enableReinitialize: true,
        onSubmit: onHandleSubmit
    });

    const { errors, touched, isSubmitting, handleSubmit, getFieldProps, setFieldValue } = formik;

    return (
        <Box sx={{ flexGrow: 1, m: 1, mx: 3, }}>

            <Stack direction="row" justifyContent="space-between" alignItems="center" sx={{ py: 1 }}>
                <Typography variant="h4" gutterBottom sx={{ my: "auto" }}>
                    Characteristic Update
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

                                    <Grid item xs={12}>
                                        <TextField
                                            select
                                            fullWidth
                                            id="characteristic-groups-select"
                                            label="Characteristic Groups"
                                            {...getFieldProps('characteristicGroupId')}
                                            error={Boolean(touched.characteristicGroupId && errors.characteristicGroupId)}
                                            helperText={touched.characteristicGroupId && errors.characteristicGroupId}
                                        >
                                            {characteristicGroups && characteristicGroups.map((item) =>
                                                <MenuItem key={item.id} value={item.id}>{item.name}</MenuItem>
                                            )}
                                        </TextField>
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
                            </Stack>
                        </Form>
                    </FormikProvider>
                </Box>
            )}
        </Box>
    )
}

export default CharacteristicUpdate;