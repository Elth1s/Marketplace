import Dialog from '@mui/material/Dialog';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Grid from "@mui/material/Grid";
import Box from '@mui/material/Box';
import Slide from '@mui/material/Slide';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';

import { TransitionProps } from '@mui/material/transitions';
import { Close } from '@mui/icons-material';
import { styled } from '@mui/system';

import { FC, forwardRef, useEffect, useState } from 'react';

import { globe, mail, map_pin, phone } from '../../../assets/icons'
import { useActions } from '../../../hooks/useActions'
import { useTypedSelector } from '../../../hooks/useTypedSelector';

import { SellerContactsButton, SellerContactsButtonSecondStyle } from "../product/styled";
import { useTranslation } from 'react-i18next';

const Transition = forwardRef(function Transition(
    props: TransitionProps & {
        children: React.ReactElement<any, any>;
    },
    ref: React.Ref<unknown>,
) {
    return <Slide direction="left" ref={ref} {...props} />;
});

const Img = styled('img')({
    margin: 'auto',
    display: 'block',
    maxWidth: '100%',
    maxHeight: '100%',
});

interface Props {
    id: number,
    isMainPage: boolean
}

const SellerInfo: FC<Props> = ({ id, isMainPage }) => {
    const { t } = useTranslation();

    const { ShopInfoFromProduct } = useActions();
    const { shopInfo } = useTypedSelector(state => state.shopInfo);
    const [open, setOpen] = useState(false);

    const handleClickOpen = async () => {
        try {
            await ShopInfoFromProduct(id);
        } catch (ex) {
        }
        setOpen(true);
    };

    const handleClickClose = () => {
        setOpen(false);
    };

    return (
        <>
            {isMainPage
                ? <SellerContactsButton color="secondary" variant="outlined" sx={{ mt: "41px" }} onClick={handleClickOpen}>{t("pages.product.sellerContacts")}</SellerContactsButton>
                : <SellerContactsButtonSecondStyle fullWidth color="secondary" variant="outlined" sx={{ mt: "26px" }} onClick={handleClickOpen}>{t("pages.product.sellerContacts")}</SellerContactsButtonSecondStyle>
            }
            <Dialog
                open={open}
                maxWidth="sm"
                fullWidth={true}
                onClose={handleClickClose}
                TransitionComponent={Transition}
                PaperProps={{
                    sx: {minWidth: { sm: "660px" }},
                    style: { borderRadius: 10 }
                }}>
                <DialogTitle sx={{ p: "34px 28px" }}>
                    <Box sx={{
                        display: "flex",
                        justifyContent: "space-between",
                        alignItems: "flex-start"
                    }}>
                        <Typography sx={{ fontSize: "30px", lineHeight: "38px" }}>Продавець</Typography>
                        <IconButton aria-label="close" onClick={handleClickClose}>
                            <Close />
                        </IconButton>
                    </Box>
                </DialogTitle>
                <DialogContent sx={{ p: "34px 28px" }}>
                    <Grid container>
                        <Grid item sx={{ m: "11px 0 30px" }}>
                            <Box sx={{ display: "flex", alignItems: "center" }}>
                                <Box sx={{ width: 90, height: 90 }}>
                                    <Img src={shopInfo.photo} alt={"image shop"} />
                                </Box>
                                <Typography variant='h5' sx={{ ml: "40px" }}>{shopInfo.name}</Typography>
                            </Box>
                        </Grid>
                        <Grid container item justifyContent="space-between">
                            <Grid item xs="auto" sx={{ display: "block", mr: "10px" }}>
                                {shopInfo.phones.map((item, index) => (
                                    <Box
                                        key={index}
                                        sx={{
                                            display: "flex",
                                            justifyContent: "flex-end",
                                            mb: "30px",
                                            "&:last-child": {
                                                mb: 0,
                                            }
                                        }}
                                    >
                                        {index === 0 ? (
                                            <>
                                                <img src={phone} alt="icon phone" />
                                                <Typography variant='h5' sx={{ ml: "10px" }}>{item}</Typography>
                                            </>
                                        ) : (
                                            <Typography variant='h5' sx={{ ml: "10px" }}>{item}</Typography>
                                        )}
                                    </Box>
                                ))}
                            </Grid>
                            <Grid item xs="auto" sx={{ display: "block" }}>
                                <Box sx={{ display: "flex", mb: "30px" }}>
                                    <img src={mail} alt="icon mail" />
                                    <Typography variant='h5' sx={{ ml: "10px" }}>{shopInfo.email}</Typography>
                                </Box>
                                <Box sx={{ display: "flex", mb: "30px" }}>
                                    <img src={globe} alt="icon globe" />
                                    <Typography variant='h5' sx={{ ml: "10px" }}>{shopInfo.siteUrl}</Typography>
                                </Box>
                                <Box sx={{ display: "flex" }}>
                                    <img src={map_pin} alt="icon map pin" />
                                    <Typography variant='h5' sx={{ ml: "10px" }}>{shopInfo.adress}</Typography>
                                </Box>
                            </Grid>
                        </Grid>
                    </Grid>
                </DialogContent>
            </Dialog>
        </>
    );
}

export default SellerInfo;