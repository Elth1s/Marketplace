// import Grid from '@mui/material/Grid';
// import Button from '@mui/material/Button';

// import { FC, useState } from "react";
// import { useFormik } from "formik";

// import { useActions } from "../../../../hooks/useActions";

// import { fieldValidation } from "../validation";
// import { IDeliveryType } from "../types";
// import { CreateProps, ServerError } from '../../../../store/types';

// import DialogComponent from '../../../../components/Dialog';
// import { toLowerFirstLetter } from '../../../../http_comon';
// import { IconButton } from '@mui/material';
// import { white_plus } from '../../../../assets/icons';
// import { TextFieldFirstStyle } from '../../../../components/TextField/styled';
// import CropperDialog from '../../../../components/CropperDialog';

// const DeliveryTypeCreate: FC<CreateProps> = ({ afterCreate }) => {
//     const [open, setOpen] = useState(false);

//     const { CreateDeliveryType } = useActions();

//     const item: IDeliveryType = {
//         englishName: "",
//         ukrainianName: "",
//         lightIcon: "",
//         darkIcon: ""
//     };

//     const handleClickOpen = () => {
//         setOpen(true);
//     };

//     const handleClickClose = () => {
//         setOpen(false);
//     };

//     const onSaveDarkIcon = async (base64: string) => {
//         setFieldValue("darkIcon", base64)
//     };
//     const onSaveLightIcon = async (base64: string) => {
//         setFieldValue("lightIcon", base64)
//     };

//     const formik = useFormik({
//         initialValues: item,
//         validationSchema: fieldValidation,
//         onSubmit: async (values, { setFieldError, resetForm }) => {
//             try {
//                 await CreateDeliveryType(values);
//                 afterCreate();
//                 resetForm();
//                 handleClickClose();
//             } catch (ex) {
//                 const serverErrors = ex as ServerError;
//                 if (serverErrors.errors)
//                     Object.entries(serverErrors.errors).forEach(([key, value]) => {
//                         if (Array.isArray(value)) {
//                             let message = "";
//                             value.forEach((item) => {
//                                 message += `${item} `;
//                             });
//                             setFieldError(toLowerFirstLetter(key), message);
//                         }
//                     });
//             }
//         }
//     });

//     const { errors, touched, isSubmitting, handleSubmit, getFieldProps, setFieldValue } = formik;

//     return (
//         <DialogComponent
//             open={open}
//             onClose={handleClickClose}
//             button={
//                 <IconButton
//                     sx={{ borderRadius: '12px', background: "#F45626", "&:hover": { background: "#CB2525" }, "&& .MuiTouchRipple-child": { backgroundColor: "transparent" } }}
//                     size="large"
//                     color="inherit"
//                     onClick={handleClickOpen}
//                 >
//                     <img
//                         style={{ width: "30px" }}
//                         src={white_plus}
//                         alt="icon"
//                     />
//                 </IconButton>
//             }

//             formik={formik}
//             isSubmitting={isSubmitting}
//             handleSubmit={handleSubmit}

//             dialogTitle="Create delivery type"
//             dialogBtnConfirm="Create"

//             dialogContent={
//                 <Grid container spacing={2}>
//                     <Grid item xs={12}>
//                         <TextFieldFirstStyle
//                             fullWidth
//                             autoComplete="englishName"
//                             variant="standard"
//                             type="text"
//                             label="English name"
//                             {...getFieldProps('englishName')}
//                             error={Boolean(touched.englishName && errors.englishName)}
//                             helperText={touched.englishName && errors.englishName}
//                         />
//                     </Grid>
//                     <Grid item xs={12}>
//                         <TextFieldFirstStyle
//                             fullWidth
//                             autoComplete="ukrainianName"
//                             variant="standard"
//                             type="text"
//                             label="Ukrainian name"
//                             {...getFieldProps('ukrainianName')}
//                             error={Boolean(touched.ukrainianName && errors.ukrainianName)}
//                             helperText={touched.ukrainianName && errors.ukrainianName}
//                         />
//                     </Grid>
//                     <Grid item xs={3}>
//                         <CropperDialog
//                             imgSrc={formik.values.lightIcon}
//                             onDialogSave={onSaveLightIcon}
//                         />
//                     </Grid>
//                     <Grid item xs={3}>
//                         <CropperDialog
//                             imgSrc={formik.values.darkIcon}
//                             onDialogSave={onSaveDarkIcon}
//                         />
//                     </Grid>
//                 </Grid>
//             }
//         />
//     )
// }

// export default DeliveryTypeCreate;

import React from 'react'

type Props = {}

const index = (props: Props) => {
    return (
        <div>index</div>
    )
}

export default index