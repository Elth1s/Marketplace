import {
    Dialog,
    DialogContent,
    DialogTitle,
    Divider,
    Grid, IconButton, Slide, Typography,
} from "@mui/material";
import { TransitionProps } from "@mui/material/transitions";
import { Close, Visibility } from "@mui/icons-material";

import { useState, FC, forwardRef } from "react";
import { ShowProps } from "../../../../store/types";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";
import { useActions } from "../../../../hooks/useActions";

const Transition = forwardRef(function Transition(
    props: TransitionProps & {
        children: React.ReactElement<any, any>;
    },
    ref: React.Ref<unknown>,
) {
    return <Slide direction="left" ref={ref} {...props} />;
});

const ShowShop: FC<ShowProps> = ({ id }) => {
    const [open, setOpen] = useState(false);

    const { GetByIdShop } = useActions();
    const { selectedShop } = useTypedSelector((store) => store.shop);

    const handleClickOpen = async () => {
        await GetByIdShop(id);
        setOpen(true);
    };

    const handleClickClose = () => {

        setOpen(false);
    };


    return (
        <>
            <Visibility onClick={() => handleClickOpen()} />
            <Dialog
                open={open}
                maxWidth="sm"
                fullWidth={true}
                onClose={handleClickClose}
                TransitionComponent={Transition}
                PaperProps={{
                    sx: {
                        width: {
                            sm: "50rem"
                        }
                    },
                    style: { borderRadius: 12 }
                }}>
                <DialogTitle sx={{ m: 0, px: 3 }}>
                    Show shop
                    <IconButton
                        aria-label="close"
                        onClick={handleClickClose}
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
                    <Grid container rowSpacing={2}>
                        <Grid item xs={12}>
                            <Typography variant="h4" color="initial">{selectedShop.name}</Typography>
                            <Typography variant="h5" color="initial">{selectedShop.description}</Typography>
                        </Grid>
                        <Grid item xs={12}>
                            <Typography variant="body1" color="initial">{selectedShop.siteUrl}</Typography>
                            <Typography variant="body1" color="initial">{selectedShop.email}</Typography>
                            {selectedShop.phones && selectedShop.phones.map((phone, index) => {
                                return (
                                    <Typography variant="body1" color="initial">{phone}</Typography>
                                )
                            })}
                        </Grid>
                        <Grid item xs={12}>
                            <Typography variant="body1" color="initial">{selectedShop.cityName}, {selectedShop.countryName} </Typography>
                        </Grid>
                        <Grid item xs={12}>
                            <Typography variant="body1" color="initial">{selectedShop.userFullName}</Typography>
                        </Grid>
                    </Grid>
                </DialogContent>
            </Dialog>
        </>
    )
}
export default ShowShop;