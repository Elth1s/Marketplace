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


const img = "https://images.unsplash.com/photo-1607936854279-55e8a4c64888?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=52&q=25"

const ordersForUserTest = [
    {
        id: "10",
        orderStatusName: "Виконано",
        totalPrice: "27000",
        deliveryType: "Самовивіз з Нової Пошти",
        address: "Рівненська обл., Острог, вул. Князів Острозьких, б.3",
        consumerFirstName: "Гандзілевська",
        consumerSecondName: "Маргарита",
        consumerPhone: "+380680580423",
        consumerEmail: "margosjka9119@gmail.com",
        products: [
            { image: { img }, price: "26999", count: "1" },
            { image: { img }, price: "26999", count: "1" },
        ],
        paymentType: "При отримані товару",
        deliveryPrice: "100"
    },
    {
        id: "10",
        orderStatusName: "Виконано",
        totalPrice: "27000",
        deliveryType: "Самовивіз з Нової Пошти",
        address: "Рівненська обл., Острог, вул. Князів Острозьких, б.3",
        consumerFirstName: "Гандзілевська",
        consumerSecondName: "Маргарита",
        consumerPhone: "+380680580423",
        consumerEmail: "margosjka9119@gmail.com",
        products: [
            { image: { img }, price: "26999", count: "1" },
            { image: { img }, price: "26999", count: "1" },
            { image: { img }, price: "26999", count: "1" },
            { image: { img }, price: "26999", count: "1" },
        ],
        paymentType: "При отримані товару",
        deliveryPrice: "100"
    },
    {
        id: "10",
        orderStatusName: "Виконано",
        totalPrice: "27000",
        deliveryType: "Самовивіз з Нової Пошти",
        address: "Рівненська обл., Острог, вул. Князів Острозьких, б.3",
        consumerFirstName: "Гандзілевська",
        consumerSecondName: "Маргарита",
        consumerPhone: "+380680580423",
        consumerEmail: "margosjka9119@gmail.com",
        products: [
            { image: { img }, price: "26999", count: "1" },
        ],
        paymentType: "При отримані товару",
        deliveryPrice: "100"
    }
]

const Order = () => {
    const { t } = useTranslation();

    const { ordersForUser } = useTypedSelector(state => state.profile);
    const { GetOrderForUser } = useActions();

    useEffect(() => {
        document.title = `${t("pages.ordering.title")}`;
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
            <PaperStyled sx={{ boxShadow: "none", margin: "0px 50px" }}>
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
                                <Grid item xs={6}>
                                    <Typography variant="h5">{item.id}</Typography>
                                    <Typography variant="h5">{item.orderStatusName}</Typography>
                                </Grid>
                                <Grid item xs={6}>
                                    <Typography variant="h5">{t("pages.user.order.totalPrice")}</Typography>
                                    <Typography variant="h5">
                                        27000
                                        {/* {item.totalPrice} */}
                                    </Typography>
                                </Grid>
                            </Grid>
                        </AccordionSummary>
                        <AccordionDetails sx={{ padding: "12px 0px 8px" }}>
                            <Grid container>
                                <Grid item xs={6}>
                                    <Typography variant="subtitle1" sx={{ mb: "20px" }}>{item.deliveryType}</Typography>
                                    <Typography variant="subtitle1" sx={{ mb: "20px" }}>
                                        Рівненська обл., Острог, вул. Князів Острозьких, б.3
                                        {/* {item.address} */}
                                    </Typography>
                                    <Typography variant="subtitle1" sx={{ mb: "20px" }}>{item.consumerFirstName} {item.consumerSecondName}</Typography>
                                    <Typography variant="subtitle1" sx={{ mb: "20px" }}>{item.consumerPhone}</Typography>
                                    <Typography variant="subtitle1">{item.consumerEmail}</Typography>
                                </Grid>
                                <Grid item xs={6}>
                                    {item.orderProductsResponse?.map((orderProduct, index) => {
                                        return (
                                            <Box key={index} sx={{ display: "flex", justifyContent: "space-between", alignItems: "stretch", mb: "10px" }}>
                                                <Box sx={{ display: "flex" }}>
                                                    <Box sx={{ width: 52, height: 52, mr: "8px" }}>
                                                        <Img alt={`image `} src={orderProduct.productImage} />
                                                    </Box>
                                                    <Box sx={{ display: "flex", flexDirection: "column", justifyContent: "center" }}>
                                                        <Typography variant="subtitle1">{t("pages.user.order.price")}</Typography>
                                                        <Typography variant="subtitle1">{orderProduct.price}</Typography>
                                                    </Box>
                                                </Box>
                                                <Box sx={{ display: "flex", flexDirection: "column", justifyContent: "center" }}>
                                                    <Typography variant="subtitle1">{t("pages.user.order.count")}</Typography>
                                                    <Typography variant="subtitle1">{orderProduct.count}</Typography>
                                                </Box>
                                            </Box>
                                        )
                                    })}
                                    <Box sx={{ display: "flex", justifyContent: "space-between", mb: "20px" }}>
                                        <Typography variant="subtitle1" sx={{ fontWeight: "700" }}>{t("pages.user.order.payment")}</Typography>
                                        <Typography variant="subtitle1">
                                            Самовивіз з Нової Пошти
                                            {/* {item.paymentType} */}
                                        </Typography>
                                    </Box>
                                    <Box sx={{ display: "flex", justifyContent: "space-between", mb: "20px" }}>
                                        <Typography variant="subtitle1" sx={{ fontWeight: "700" }}>{t("pages.user.order.delivery")}</Typography>
                                        <Typography variant="subtitle1">{item.deliveryType}</Typography>
                                    </Box>
                                    <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                                        <Typography variant="subtitle1" sx={{ fontWeight: "700" }}>{t("pages.user.order.total")}</Typography>
                                        <Typography variant="subtitle1">
                                            27000
                                            {/* {item.totalPrice} */}
                                        </Typography>
                                    </Box>
                                </Grid>
                            </Grid>
                        </AccordionDetails>
                    </Accordion>
                ))}
            </PaperStyled>
        </>
    );
}

export default Order;