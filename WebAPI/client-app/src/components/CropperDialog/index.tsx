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
    Slide
} from "@mui/material";
import { Close } from "@mui/icons-material";

import { LegacyRef, forwardRef, useRef, useState, useEffect } from "react";
import Cropper from "cropperjs";

import { ICropperDialog } from "./types";

const Transition = forwardRef(function Transition(props: any, ref) {
    return <Slide direction="left" ref={ref} {...props} />;
});

const CropperDialog: React.FC<ICropperDialog> = ({ imgSrc, aspectRation = 1 / 1, onDialogSave }) => {
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

    const handleImageChange = async function (e: React.ChangeEvent<HTMLInputElement>) {
        const fileList = e.target.files;
        if (!fileList || fileList.length === 0) return;

        await selectImage(URL.createObjectURL(fileList[0]));
        e.target.value = "";
    };

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
            <Box>
                <label htmlFor="Image">
                    <img
                        src={imgSrc}
                        alt="DefaultImage"
                        style={{ width: "160px", height: "160px", cursor: "pointer", borderRadius: 7 }} />
                </label>
                <input style={{ display: "none" }} type="file" name="Image" id="Image" onChange={handleImageChange} />
            </Box>
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