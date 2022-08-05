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
    Typography
} from "@mui/material";
import { Close } from "@mui/icons-material";

import { LegacyRef, forwardRef, useRef, useState, useEffect } from "react";
import Cropper from "cropperjs";
import { TransitionProps } from "@mui/material/transitions";

import { BoxStyle } from "./styled"

import { useDropzone } from 'react-dropzone';
import { upload_cloud } from "../../assets/icons";

const Transition = forwardRef(function Transition(
    props: TransitionProps & {
        children: React.ReactElement<any, any>;
    },
    ref: React.Ref<unknown>,
) {
    return <Slide direction="left" ref={ref} {...props} />;
});

export interface ICropperDialog {
    imgSrc: string,
    aspectRation?: number,
    onDialogSave: any,
    labelId: string
}

const CropperDialog: React.FC<ICropperDialog> = ({ imgSrc, aspectRation = 1 / 1, onDialogSave, labelId }) => {
    const [cropperObj, setCropperObj] = useState<Cropper>();
    const imgRef = useRef<HTMLImageElement>(null);
    const prevRef = useRef<HTMLDivElement>();

    const [isCropperDialogOpen, setIsCropperDialogOpen] = useState(false);

    const selectImage = async (url: string) => {
        if (!cropperObj) {
            const cropper = new Cropper(imgRef.current as HTMLImageElement, {
                aspectRatio: aspectRation,
                viewMode: 1,
                dragMode: 'move',
                preview: prevRef.current,
            });
            cropper.replace(url);
            setCropperObj(cropper);
        }
        else {
            cropperObj?.replace(url);
        }

        setIsCropperDialogOpen(true);
    }

    // const handleImageChange = async function (e: React.ChangeEvent<HTMLInputElement>) {
    //     console.log("qwe")
    //     const fileList = e.target.files;
    //     if (!fileList || fileList.length === 0) return;

    //     await selectImage(URL.createObjectURL(fileList[0]));
    //     e.target.value = "";
    // };
    const { getRootProps, getInputProps } = useDropzone({
        // Note how this callback is never invoked if drop occurs on the inner dropzone
        multiple: false,
        onDrop: async (files) => {
            const fileList = files;
            if (!fileList || fileList.length === 0) return;

            await selectImage(URL.createObjectURL(fileList[0]));
        }
    });

    const cropperDialogClose = () => {
        setIsCropperDialogOpen(false);
    };

    const cropperDialogSave = async function (e: React.MouseEvent<HTMLElement>) {
        const base64 = cropperObj?.getCroppedCanvas().toDataURL() as string;
        onDialogSave(base64);
        setIsCropperDialogOpen(false);
    };


    return (
        <>
            <BoxStyle imgSrc={imgSrc} sx={{ boxShadow: 4 }}>
                {/* <label htmlFor={labelId}>
                    <img
                        src={imgSrc}
                        alt="DefaultImage"
                        style={{ width: "160px", height: "160px", cursor: "pointer", borderRadius: "12px" }} />
                </label>
                <input style={{ display: "none" }} type="file" name={labelId} id={labelId} onChange={handleImageChange} /> */}
                <div {...getRootProps({ className: 'dropzone' })}>
                    <input {...getInputProps()} />
                    {imgSrc !== ""
                        ? <img
                            src={imgSrc}
                            alt="DefaultImage"
                            style={{ width: "100px", height: "100px", borderRadius: "10px" }} />
                        : <Box sx={{ height: "100px", display: "flex", flexDirection: "column", justifyContent: "center", alignItems: "center" }}>
                            <img
                                src={upload_cloud}
                                alt="icon"
                                style={{ width: "25px", height: "25px" }} />
                            <Typography variant="subtitle1" align="center">
                                Select photo
                            </Typography>
                        </Box>}
                </div>
            </BoxStyle>
            <Dialog
                open={isCropperDialogOpen}
                TransitionComponent={Transition}
                maxWidth="lg"
                keepMounted
                onClose={cropperDialogClose}
                aria-describedby="alert-dialog-slide-description"
                PaperProps={{
                    sx: {
                        width: {
                            sm: "50rem"
                        }
                    },
                    style: { borderRadius: 12 }
                }}
            >
                <DialogTitle sx={{ m: 0, px: 3 }}>
                    Change photo
                    <IconButton
                        aria-label="close"
                        onClick={cropperDialogClose}
                        sx={{
                            position: 'absolute',
                            my: "auto",
                            right: 20,
                            top: 15,
                            borderRadius: "12px"
                        }}
                    >
                        <Close />
                    </IconButton>
                </DialogTitle>
                <DialogContent sx={{ m: 0, px: 3, pt: 0 }}>
                    <Grid container spacing={{ xs: 3 }}>
                        <Grid item lg={9} xs={12} sx={{ height: "475px" }}>
                            <Paper>
                                <img ref={imgRef}
                                    alt="SelectedImage"
                                    src={imgSrc}
                                    style={{ width: "100%", height: "450px", display: "block" }} />
                            </Paper>
                        </Grid>
                        <Grid item lg={3} xs={12}>
                            <Paper elevation={2} >
                                <div ref={prevRef as LegacyRef<HTMLDivElement>}
                                    style={{
                                        width: "170px",
                                        height: "170px",
                                        overflow: "hidden",
                                        borderRadius: "7px",
                                    }}>
                                </div>
                            </Paper>
                        </Grid>
                    </Grid>
                </DialogContent>
                <DialogActions sx={{ m: 0, p: 3, pt: 0 }}>
                    <Button autoFocus size="medium"
                        variant="contained"
                        onClick={cropperDialogSave}
                    >
                        Save changes
                    </Button>
                </DialogActions>

            </Dialog >
        </>
    )
}
export default CropperDialog;