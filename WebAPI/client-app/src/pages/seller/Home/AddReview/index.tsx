import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import Grid from "@mui/material/Grid";
import Box from '@mui/material/Box';
import Slide from '@mui/material/Slide';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';

import { TransitionProps } from '@mui/material/transitions';
import { Close } from '@mui/icons-material';
import { LoadingButton, Rating } from '@mui/lab';

import { forwardRef, useEffect, useState } from 'react';
import { Form, FormikProvider, useFormik } from "formik";

import { ServerError } from '../../../../store/types';

// import { validationFields } from '../validation';

const Transition = forwardRef(function Transition(
    props: TransitionProps & {
        children: React.ReactElement<any, any>;
    },
    ref: React.Ref<unknown>,
) {
    return <Slide direction="left" ref={ref} {...props} />;
});

interface IReview {
    fullName: string,
    email: string,
    qualityOfService: number,
    observanceOfTerms: number,
    informationRelevance: number,
    review: string,
}

const AddReview = () => {
    const [open, setOpen] = useState(false);

    const item: IReview = {
        fullName: "",
        email: "",
        qualityOfService: 0,
        observanceOfTerms: 0,
        informationRelevance: 0,
        review: "",
    };

    useEffect(() => {
        getData();
    }, [])

    const getData = async () => {

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
                        setFieldError(key.toLowerCase(), message);
                    }
                });
        }
    }
    const formik = useFormik({
        initialValues: item,
        //  validationSchema: validationFields,
        onSubmit: onHandleSubmit
    });

    const { errors, touched, isSubmitting, handleSubmit, setFieldError, getFieldProps, resetForm } = formik;

    return (
        <>
            <Button
                variant="contained"
                onClick={handleClickOpen}
                sx={{
                    fontSize: "24px",
                    lineHeight: "30px",
                    fontWeight: "600",
                    borderRadius: "10px",
                    padding: "14px 48px",
                }}
            >
                Add review
            </Button>
            <Dialog
                open={open}
                maxWidth="sm"
                fullWidth={true}
                onClose={handleClickClose}
                TransitionComponent={Transition}
                PaperProps={{
                    sx: {
                        maxWidth: { sm: "50rem" }
                    },
                    style: { borderRadius: 12 }
                }}>
                <DialogTitle sx={{ p: "34px 28px" }}>
                    <Box sx={{
                        display: "flex",
                        justifyContent: "space-between",
                        alignItems: "flex-start"
                    }}>
                        <Typography sx={{ fontSize: "30px", lineHeight: "38px" }}>Send feedback</Typography>
                        <IconButton aria-label="close" onClick={handleClickClose}>
                            <Close />
                        </IconButton>
                    </Box>
                </DialogTitle>
                <FormikProvider value={formik} >
                    <Form onSubmit={handleSubmit}>
                        <DialogContent sx={{ p: "34px 28px" }}>
                            <Grid container>
                                <Grid item container xs={12} sx={{ justifyContent: "space-between", textAlig: "center", mb: "45px" }}>
                                    <Grid item>
                                        <Typography variant="h5" sx={{ mb: "10px" }}>Quality of service</Typography>
                                        <Rating
                                            color="primary"
                                            sx={{ fontSize: "30px" }}
                                            {...getFieldProps("qualityOfService")}
                                        />
                                    </Grid>
                                    <Grid item>
                                        <Typography>Observance of terms</Typography>
                                        <Rating
                                            color="primary"
                                            sx={{ fontSize: "30px" }}
                                            {...getFieldProps("observanceOfTerms")}
                                        />
                                    </Grid>
                                    <Grid item>
                                        <Typography>Information relevance</Typography>
                                        <Rating
                                            color="primary"
                                            sx={{ fontSize: "30px" }}
                                            {...getFieldProps("informationRelevance")}
                                        />
                                    </Grid>
                                </Grid>
                                <Grid xs={12} sx={{ mb: "25px" }}>
                                    <TextField
                                        fullWidth
                                        variant="outlined"
                                        type="text"
                                        label="Full Name"
                                        error={Boolean(touched.fullName && errors.fullName)}
                                        helperText={touched.fullName && errors.fullName}
                                        {...getFieldProps("fullName")}
                                    />
                                </Grid>
                                <Grid xs={12} sx={{ mb: "45px" }}>
                                    <TextField
                                        fullWidth
                                        variant="outlined"
                                        type="email"
                                        label="Email"
                                        error={Boolean(touched.email && errors.email)}
                                        helperText={touched.email && errors.email}
                                        {...getFieldProps("email")}
                                    />
                                </Grid>
                                <Grid xs={12} sx={{ mb: "25px" }}>
                                    <TextField
                                        fullWidth
                                        multiline
                                        rows={5}
                                        variant="outlined"
                                        type="text"
                                        label="Review"
                                        error={Boolean(touched.review && errors.review)}
                                        helperText={touched.review && errors.review}
                                        {...getFieldProps("review")}
                                    />
                                </Grid>
                            </Grid>
                        </DialogContent>
                        <DialogActions sx={{ m: 0, p: 3, pt: 0 }}>
                            <LoadingButton
                                fullWidth
                                type="submit"
                                color="secondary"
                                variant="contained"
                                loading={isSubmitting}
                                sx={{
                                    fontSize: "20px",
                                    lineHeight: "25px",
                                    py: "15px",
                                }}
                            >
                                Send
                            </LoadingButton>
                        </DialogActions>
                    </Form>
                </FormikProvider>
            </Dialog>
        </>
    )
}

export default AddReview;