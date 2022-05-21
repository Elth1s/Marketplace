
import { Box, Grid, Stack, Typography, CircularProgress, TextField, FormControl, InputLabel, Select, MenuItem } from "@mui/material";

import { useEffect, useState } from "react";
import { useNavigate } from "react-router";

import { LoadingButton } from "@mui/lab";

import { Form, FormikProvider, useFormik } from "formik";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { validationFields } from "../validation";
import { ICharacteristicGroup } from "../types";

const CharacteristicGroupCreate = () => {
    const { CreateCharacteristicGroup } = useActions();
    const [loading, setLoading] = useState<boolean>(false);

    const category: ICharacteristicGroup = {
        name: ""
    }

    const navigator = useNavigate();

    useEffect(() => {

    }, []);

    const onHandleSubmit = async (values: ICharacteristicGroup) => {
        try {
            await CreateCharacteristicGroup(values);
            navigator("/characteristicGroup");
        } catch (ex) {

        }
    }

    const formik = useFormik({
        initialValues: category,
        validationSchema: validationFields,
        onSubmit: onHandleSubmit
    });

    const { errors, touched, isSubmitting, handleSubmit, getFieldProps } = formik;

    return (
        <Box sx={{ flexGrow: 1, m: 1, mx: 3, }}>
            <Stack direction="row" justifyContent="space-between" alignItems="center" sx={{ py: 1 }}>
                <Typography variant="h4" gutterBottom sx={{ my: "auto" }}>
                    Characteristic Group Create
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
                                    <Grid item xs={12} mt={3} display="flex" justifyContent="space-between" >
                                        <LoadingButton
                                            sx={{ paddingX: "35px" }}
                                            size="large"
                                            type="submit"
                                            variant="contained"
                                            loading={isSubmitting}
                                        >
                                            Create
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

export default CharacteristicGroupCreate;