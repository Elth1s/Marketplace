import Accordion from "@mui/material/Accordion";
import AccordionDetails from "@mui/material/AccordionDetails";
import AccordionSummary from "@mui/material/AccordionSummary";
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";

import { useEffect } from "react";
import { useTranslation } from "react-i18next";

import { useActions } from "../../../hooks/useActions";
import { useTypedSelector } from "../../../hooks/useTypedSelector";

import { PaperStyled, Img } from "../styled";
import Grid from "@mui/material/Grid";
import { Button, Paper } from "@mui/material";
import { small_empty } from "../../../assets/backgrounds";
import LinkRouter from "../../../components/LinkRouter";

const Order = () => {
    const { t } = useTranslation();

    const { GetOrderForUser, CancelOrder } = useActions();
    const { ordersForUser } = useTypedSelector(state => state.profile);

    useEffect(() => {
        document.title = `${t("pages.user.order.title")}`;
        getData();
    }, [])


    const getData = async () => {
        try {
            await GetOrderForUser()
        } catch (ex) {
        }
    };

    return (
        <>
            <Typography variant="h1" color="primary" sx={{ mb: "27px" }}>{t("pages.user.order.title")}</Typography>
            {ordersForUser?.map((item, index) => (
                <Accordion key={index}
                    sx={{
                        borderBottom: "1px solid #7E7E7E",
                        boxShadow: "none",
                        "&:first-of-type": {
                            borderTop: "1px solid #7E7E7E",
                            borderRadius: "0",
                        },
                        "&:last-of-type": {
                            borderRadius: "0",
                        },
                        "&:before": {
                            content: "none"
                        }
                    }}>
                    <AccordionSummary
                        expandIcon={<ExpandMoreIcon />}
                        aria-controls="panel1a-content"
                        id="panel1a-header"
                        sx={{
                            padding: "0",
                            margin: "8px",
                            "&:before": {
                                content: '""',
                                background: "#F45626",
                                width: "13px",
                                height: "48px",
                                borderRadius: "9px",
                                marginRight: "18px",
                            },
                            "& .MuiAccordionSummary-content": {
                                margin: "0",
                                "&.Mui-expanded": { margin: "0", }
                            }
                        }}>
                        <Grid container>
                            <Grid item xs={4}>
                                <Typography variant="h5" color="inherit">№{item.id} {t("pages.user.order.from")} {item.date}</Typography>
                                <Typography variant="h5" color="inherit">{item.orderStatusName}</Typography>
                            </Grid>
                            <Grid item xs={4}>
                                <Typography variant="h5" color="inherit">{t("pages.user.order.totalPrice")}</Typography>
                                <Typography variant="h5" color="inherit">
                                    {item.totalPrice} {t("currency")}
                                </Typography>
                            </Grid>
                            <Grid item xs={4}>
                                <Paper elevation={0} sx={{ backgroundColor: "inherit", display: "flex", flexDirection: 'row-reverse', overflow: 'hidden', '&::-webkit-scrollbar': { display: "none" } }} >
                                    {item.orderProductsResponse?.map((orderProduct, index) => {
                                        return (
                                            (orderProduct.productImage != "" && <Box key={`preview_image_${index}`} >
                                                <Box sx={{ width: 52, height: 52, mr: "8px" }}>
                                                    <Img alt={`image `} src={orderProduct.productImage} />
                                                </Box>
                                            </Box>)
                                        )
                                    })}
                                </Paper>
                            </Grid>
                        </Grid>
                    </AccordionSummary>
                    <AccordionDetails sx={{ padding: "12px 0px 8px" }}>
                        <Grid container>
                            <Grid item xs={4}>
                                <Typography variant="subtitle1" color="inherit" sx={{ mb: "20px" }}>{item.deliveryType}</Typography>
                                <Typography variant="subtitle1" color="inherit" sx={{ mb: "20px" }}>
                                    {item.city} {item.department}
                                </Typography>
                                <Typography variant="subtitle1" color="inherit" sx={{ mb: "20px" }}>{item.consumerFirstName} {item.consumerSecondName}</Typography>
                                <Typography variant="subtitle1" color="inherit" sx={{ mb: "20px" }}>{item.consumerPhone}</Typography>
                                <Typography variant="subtitle1" color="inherit">{item.consumerEmail}</Typography>
                            </Grid>
                            <Grid item xs={8} sx={{ pl: "18px" }}>
                                {item.orderProductsResponse?.map((orderProduct, index) => {
                                    return (
                                        <Box key={index} sx={{ display: "flex", justifyContent: "space-between", alignItems: "stretch", mb: "10px" }}>
                                            <Box sx={{ display: "flex" }}>
                                                <Box sx={{ width: 52, height: 52, mr: "10px" }}>
                                                    <Img alt={`image `} src={orderProduct.productImage != "" ? orderProduct.productImage : small_empty} />
                                                </Box>
                                                <Box sx={{ display: "flex", alignItems: "center" }}>
                                                    <LinkRouter underline="hover" color="inherit" to={`/product/${orderProduct.productUrlSlug}`}>
                                                        <Typography color="inherit" variant="subtitle1" sx={{ width: "200px", mr: "10px" }}>{orderProduct.productName}</Typography>
                                                    </LinkRouter>
                                                </Box>
                                            </Box>
                                            <Box sx={{ display: "flex", flexDirection: "column", justifyContent: "center", width: "90px" }}>
                                                <Typography color="inherit" variant="subtitle1">{t("pages.user.order.price")}</Typography>
                                                <Typography color="inherit" variant="subtitle1">{orderProduct.price} {t("currency")}</Typography>
                                            </Box>
                                            <Box sx={{ display: "flex", flexDirection: "column", justifyContent: "center" }}>
                                                <Typography color="inherit" variant="subtitle1">{t("pages.user.order.count")}</Typography>
                                                <Typography color="inherit" variant="subtitle1">{orderProduct.count}</Typography>
                                            </Box>
                                        </Box>
                                    )
                                })}
                                <Box sx={{ display: "flex", justifyContent: "space-between", mb: "20px" }}>
                                    <Typography variant="subtitle1" color="inherit" sx={{ fontWeight: "700" }}>{t("pages.user.order.payment")}</Typography>
                                    <Typography variant="subtitle1" color="inherit">
                                        {t("pages.ordering.paymentUponReceipt")}
                                    </Typography>
                                </Box>
                                <Box sx={{ display: "flex", justifyContent: "space-between", mb: "20px" }}>
                                    <Typography variant="subtitle1" color="inherit" sx={{ fontWeight: "700" }}>{t("pages.user.order.delivery")}</Typography>
                                    <Typography variant="subtitle1" color="inherit">{t("pages.ordering.accordingCarrierTariffs")}</Typography>
                                </Box>
                                <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                                    <Typography variant="subtitle1" color="inherit" sx={{ fontWeight: "700" }}>{t("pages.user.order.total")}</Typography>
                                    <Typography variant="subtitle1" color="inherit">
                                        {item.totalPrice} {t("currency")}
                                    </Typography>
                                </Box>
                                {item.canUpdate && <Box sx={{ display: "flex", justifyContent: "end" }}>
                                    <Button
                                        color="primary"
                                        variant="outlined"
                                        sx={{
                                            width: "auto",
                                            px: "15.5px",
                                            py: "8.5px",
                                            textTransform: "none",
                                            borderRadius: "10px",
                                            fontSize: "18px",
                                            height: "40px",
                                            border: "1px solid #F45626",
                                            mt: "10px"
                                        }}
                                        onClick={async () => {
                                            await CancelOrder(item.id)
                                            await GetOrderForUser()
                                        }}
                                    >
                                        {t("pages.user.order.cancel")}
                                    </Button>
                                </Box>}
                            </Grid>
                        </Grid>
                    </AccordionDetails>
                </Accordion>
            ))
            }
        </>
    );
}

export default Order;