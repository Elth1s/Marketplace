import { LoadingButton } from "@mui/lab";
import {
    Box,
    Button,
    Checkbox,
    CssBaseline,
    FormControl,
    FormControlLabel,
    Grid,
    InputLabel,
    MenuItem,
    Select,
    TextField,
    Typography,
    Container,
    useTheme
} from "@mui/material";
import { Form, FormikProvider, useFormik } from "formik";
import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { empty } from "../../../assets/backgrounds";

import { black_map_pin } from "../../../assets/icons";
import { dark_logo, light_logo } from "../../../assets/logos";
import LinkRouter from "../../../components/LinkRouter";
import { TextFieldSecondStyle } from "../../../components/TextField/styled";
import { useActions } from "../../../hooks/useActions";
import { useTypedSelector } from "../../../hooks/useTypedSelector";
import { toLowerFirstLetter } from "../../../http_comon";
import { ServerError } from "../../../store/types";
import { IOrderCreate } from "../types";
import { OrderSchema } from "../validation";

const Ordering = () => {
    const { t } = useTranslation();
    const { palette } = useTheme();

    const { orderProducts } = useTypedSelector(state => state.profile);
    const { GetOrderProducts } = useActions();

    const [amountPayable, setAmountPayable] = useState<number>(0);
    const [ordersCount, setOrdersCount] = useState<number>(0);

    useEffect(() => {
        document.title = `${t("pages.ordering.title")}`;

        getData();
    }, [orderProducts])

    const getData = async () => {
        try {
            await GetOrderProducts()
            await sumAmountPayableWithCount()
        } catch (ex) {
        }
    };

    const orderModel: IOrderCreate = {
        consumerFirstName: "",
        consumerSecondName: "",
        consumerEmail: "",
        consumerPhone: "",
        orders: []
    };

    const formik = useFormik({
        initialValues: orderModel,
        validationSchema: OrderSchema,
        onSubmit: async (values, { setFieldError }) => {
            try {

            }
            catch (exception) {
                const serverErrors = exception as ServerError;
                if (serverErrors.errors)
                    Object.entries(serverErrors.errors).forEach(([key, value]) => {
                        if (Array.isArray(value)) {
                            let message = "";
                            value.forEach((item) => {
                                message += `${item} `;
                            });
                            setFieldError(toLowerFirstLetter(key), message);
                        }
                    });
            }

        }
    });

    const sumAmountPayableWithCount = async () => {
        let tempAmountPayable = 0;
        let tempCount = 0;

        orderProducts.forEach(orderItem => {
            tempAmountPayable += orderItem.totalPrice;
            orderItem.basketItems.forEach(basketItem => {
                tempCount += basketItem.count
            });
        });
        setAmountPayable(tempAmountPayable)
        setOrdersCount(tempCount)
    }

    const { errors, touched, isSubmitting, handleSubmit, getFieldProps } = formik;

    return (
        <Container sx={{ maxWidth: { xl: "xl", lg: "lg", md: "md" } }}>
            <Box sx={{ mt: "26px" }}>
                <LinkRouter underline="none" color="inherit" to="/" >
                    <img
                        style={{ cursor: "pointer", height: "82px" }}
                        src={palette.mode == "dark" ? dark_logo : light_logo}
                        alt="logo"
                    />
                </LinkRouter>
            </Box>
            <FormikProvider value={formik} >
                <Form autoComplete="off" noValidate onSubmit={handleSubmit} >
                    <Typography variant="h1" sx={{ mt: "25px" }}>
                        {t("pages.ordering.title")}
                    </Typography>
                    <Grid container sx={{ mt: "35px" }}>
                        <Grid item container xs={8} rowSpacing="35px">
                            <Grid item xs={12} sx={{ mt: "15px" }}>
                                <Typography variant="h2">{t("pages.ordering.contactInfo")}</Typography>
                            </Grid>
                            <Grid item xs={4}>
                                <TextFieldSecondStyle
                                    fullWidth
                                    variant="outlined"
                                    type="text"
                                    placeholder="First name"
                                    {...getFieldProps('consumerFirstName')}
                                    error={Boolean(touched.consumerFirstName && errors.consumerFirstName)}
                                    helperText={touched.consumerFirstName && errors.consumerFirstName}
                                />
                            </Grid>
                            <Grid item xs={2} />
                            <Grid item xs={4}>
                                <TextFieldSecondStyle
                                    fullWidth
                                    variant="outlined"
                                    type="text"
                                    placeholder="Second name"
                                    {...getFieldProps('consumerSecondName')}
                                    error={Boolean(touched.consumerSecondName && errors.consumerSecondName)}
                                    helperText={touched.consumerSecondName && errors.consumerSecondName}
                                />
                            </Grid>
                            <Grid item xs={4}>
                                <TextFieldSecondStyle
                                    fullWidth
                                    variant="outlined"
                                    type="text"
                                    placeholder="Email"
                                    {...getFieldProps('consumerEmail')}
                                    error={Boolean(touched.consumerEmail && errors.consumerEmail)}
                                    helperText={touched.consumerEmail && errors.consumerEmail}
                                />
                            </Grid>
                            <Grid item xs={2} />
                            <Grid item xs={4}>
                                <TextFieldSecondStyle
                                    fullWidth
                                    variant="outlined"
                                    type="text"
                                    placeholder="Phone number"
                                    {...getFieldProps('consumerPhone')}
                                    error={Boolean(touched.consumerPhone && errors.consumerPhone)}
                                    helperText={touched.consumerPhone && errors.consumerPhone}
                                />
                            </Grid>
                            <Grid item xs={12} sx={{ mt: "15px" }}>
                                {orderProducts.map((item, index) => {

                                    return (
                                        <Box key={`order_${index}`}>
                                            <Box sx={{ display: "flex" }}>
                                                <Typography variant="h2" sx={{ mr: "180px" }}>
                                                    {t("pages.ordering.order")} №{index + 1}
                                                </Typography>
                                                <Typography variant="h4">
                                                    {t("pages.ordering.forTheSum")} {item.totalPrice} &#8372;
                                                </Typography>
                                            </Box>
                                            <Typography variant="h3" sx={{ mt: "15px" }}>
                                                {t("pages.ordering.sellerProducts")} {item.shopName}
                                            </Typography>
                                            {item.basketItems.map((basketItem, index) => {

                                                return (
                                                    <Box key={`order_basket_item_${index}`} sx={{ display: "flex", justifyContent: "space-between", alignItems: "center", py: "15px" }}>
                                                        <Box sx={{ display: "flex", alignItems: "center" }}>
                                                            <img
                                                                style={{ width: "120px", height: "120px", objectFit: "contain" }}
                                                                src={basketItem.productImage != "" ? basketItem.productImage : empty}
                                                                alt="productImage"
                                                            />
                                                            <LinkRouter underline="hover" color="inherit" to={`/product/${basketItem.productUrlSlug}`}>
                                                                <Typography variant="h4" sx={{ width: "550px" }}>
                                                                    {basketItem.productName}
                                                                </Typography>
                                                            </LinkRouter>
                                                        </Box>
                                                        <Box sx={{ display: "flex", flexDirection: "column" }}>
                                                            <Typography variant="h4">
                                                                {t("pages.ordering.price")}
                                                            </Typography>
                                                            <Typography variant="h4" align="center">
                                                                {basketItem.productPrice} &#8372;
                                                            </Typography>
                                                        </Box>
                                                        <Box sx={{ display: "flex", flexDirection: "column" }}>
                                                            <Typography variant="h4">
                                                                {t("pages.ordering.count")}
                                                            </Typography>
                                                            <Typography variant="h4" align="center">
                                                                {basketItem.count}
                                                            </Typography>
                                                        </Box>
                                                    </Box>
                                                )
                                            })}
                                        </Box>
                                    )
                                })}
                            </Grid>
                        </Grid>
                        <Grid item xs={1} />
                        <Grid item xs={3} >
                            <Box sx={{ position: "sticky", top: "20px" }}>
                                <Typography variant="h2">
                                    {t("pages.ordering.inGeneral")}
                                </Typography>
                                <Box sx={{ display: "flex", justifyContent: "space-between", mt: "35px" }}>
                                    <Typography variant="h4" fontWeight={700}>
                                        {ordersCount} {t("pages.ordering.countProductsOne")} {t("pages.ordering.forTheSum")}:
                                    </Typography>
                                    <Typography variant="h4">
                                        {amountPayable} &#8372;
                                    </Typography>
                                </Box>
                                <Box sx={{ display: "flex", justifyContent: "space-between", mt: "20px" }}>
                                    <Typography variant="h4" fontWeight={700} sx={{ height: "25px", my: "auto" }}>
                                        {t("pages.ordering.shippingCost")}:
                                    </Typography>
                                    <Typography variant="h4" align="right" sx={{ width: "200px" }}>
                                        {t("pages.ordering.accordingCarrierTariffs")}
                                    </Typography>
                                </Box>
                                <Box sx={{ display: "flex", justifyContent: "space-between", mt: "20px" }}>
                                    <Typography variant="h4" fontWeight={700}>
                                        {t("pages.ordering.amountPayable")}:
                                    </Typography>
                                    <Typography variant="h4">
                                        {amountPayable} &#8372;
                                    </Typography>
                                </Box>
                                <LoadingButton
                                    fullWidth
                                    color="secondary"
                                    variant="contained"
                                    loading={isSubmitting}
                                    type="submit"
                                    sx={{ height: "60px", fontSize: "24px", py: "20px", mt: "35px", textTransform: "none" }}
                                >
                                    {t("pages.ordering.checkout")}
                                </LoadingButton>
                            </Box>
                        </Grid>
                    </Grid>
                </Form>
            </FormikProvider>
        </Container>
    );
};

export default Ordering;
