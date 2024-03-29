import { ExpandMore } from "@mui/icons-material";
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
    useTheme,
    Accordion,
    AccordionSummary,
    AccordionDetails
} from "@mui/material";
import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { Navigate, useNavigate } from "react-router-dom";

import { Form, FormikProvider, useFormik } from "formik";
import * as Yup from 'yup';

import { small_empty } from "../../../assets/backgrounds";

import { black_map_pin } from "../../../assets/icons";
import { dark_logo, light_logo } from "../../../assets/logos";
import LinkRouter from "../../../components/LinkRouter";
import { TextFieldFirstStyle, TextFieldSecondStyle } from "../../../components/TextField/styled";
import { useActions } from "../../../hooks/useActions";
import { useTypedSelector } from "../../../hooks/useTypedSelector";
import { toLowerFirstLetter } from "../../../http_comon";
import { ServerError } from "../../../store/types";
import { IOrderCreate } from "../types";

const Ordering = () => {
    const { t } = useTranslation();
    const { palette } = useTheme();

    const { GetOrderProducts, CreateOrder } = useActions();
    const { orderProducts } = useTypedSelector(state => state.profile);
    const { user } = useTypedSelector(state => state.auth)

    const navigate = useNavigate();

    const [currentOrder, setCurrentOrder] = useState<number>(1);

    useEffect(() => {
        document.title = `${t("pages.ordering.title")}`;

        getData();
    }, [])

    useEffect(() => {
    }, [currentOrder])

    const getData = async () => {
        try {
            await GetOrderProducts()
        } catch (ex) {
        }
    };

    const orderModel: IOrderCreate = {
        consumerFirstName: user.firstName,
        consumerSecondName: user.secondName,
        consumerEmail: user.isEmailExist ? user.emailOrPhone : "",
        consumerPhone: !user.isEmailExist ? user.emailOrPhone : "",
        basketItems: [],
        deliveryTypeId: "",
        city: "",
        department: ""
    };

    const OrderSchema = Yup.object().shape({
        consumerFirstName: Yup.string().required().min(2).max(15).label(t('validationProps.firstName')),
        consumerSecondName: Yup.string().required().min(2).max(40).label(t('validationProps.secondName')),
        consumerEmail: Yup.string().email().label(t('validationProps.email')),
        consumerPhone: Yup.string().required().phone().label(t('validationProps.phone')),
        deliveryTypeId: Yup.mixed().required().label(t('validationProps.deliveryType')),
        city: Yup.string().required().label(t('validationProps.city')),
        department: Yup.string().required().label(t('validationProps.department')),
    });

    const formik = useFormik({
        initialValues: orderModel,
        validationSchema: OrderSchema,
        onSubmit: async (values, { setFieldError }) => {
            try {
                values.basketItems = orderProducts[currentOrder - 1].basketItems;
                await CreateOrder(values);
                if (orderProducts.length == currentOrder)
                    navigate("/")
                var tempCurrentOrder = currentOrder + 1;
                setCurrentOrder(tempCurrentOrder);
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

    const [expanded, setExpanded] = useState<number | false>(false);

    const handleChange = (index: number) => (event: React.SyntheticEvent, isExpanded: boolean) => {
        setExpanded(isExpanded ? index : false);
    };

    const lastCharOfCountProducts = (countProducts: number) => {
        if (!countProducts)
            return;

        let stringCountProducts = countProducts.toString();
        let lastChar = stringCountProducts[stringCountProducts.length - 1];
        let twoLastChar = stringCountProducts.slice(-2);
        if (twoLastChar == "12" || twoLastChar == "13" || twoLastChar == "14")
            return false
        else if (lastChar == "2" || lastChar == "3" || lastChar == "4")
            return true;
        else
            return false
    };

    const { errors, touched, isSubmitting, handleSubmit, getFieldProps } = formik;

    return (
        <Container sx={{ maxWidth: { xl: "xl", lg: "lg", md: "md" } }}>
            <Box sx={{ mt: "11px" }}>
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
                    <Typography variant="h1" color="inherit" sx={{ mt: "25px" }}>
                        {t("pages.ordering.title")}
                    </Typography>
                    <Grid container sx={{ mt: "35px" }}>
                        <Grid item container xs={8} rowSpacing="35px">
                            <Grid item xs={12} sx={{ mt: "15px" }}>
                                <Typography variant="h2" color="inherit">{t("pages.ordering.contactInfo")}</Typography>
                            </Grid>
                            <Grid item xs={4}>
                                <TextFieldSecondStyle
                                    fullWidth
                                    variant="outlined"
                                    type="text"
                                    placeholder={t('validationProps.firstName')}
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
                                    placeholder={t('validationProps.secondName')}
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
                                    placeholder={t('validationProps.email')}
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
                                    placeholder={t('validationProps.phone')}
                                    {...getFieldProps('consumerPhone')}
                                    error={Boolean(touched.consumerPhone && errors.consumerPhone)}
                                    helperText={touched.consumerPhone && errors.consumerPhone}
                                />
                            </Grid>
                            <Grid item xs={12} sx={{ mt: "15px" }}>
                                {orderProducts.map((item, index) => {
                                    let isOpen = (index + 1) == currentOrder;
                                    return (
                                        <>
                                            {isOpen
                                                ? <Accordion expanded={isOpen} key={`order_${index}`} sx={{ boxShadow: 0, "&:before": { background: "inherit" } }}>
                                                    <AccordionSummary expandIcon={<ExpandMore />}>
                                                        <Box
                                                            sx={{
                                                                display: "flex",
                                                                alignItems: "end"
                                                            }}
                                                        >
                                                            <Typography variant="h2" color="inherit" sx={{ mr: "180px" }}>
                                                                {t("pages.ordering.order")} №{index + 1}
                                                            </Typography>
                                                            <Typography variant="h4" color="inherit">
                                                                {t("pages.ordering.forTheSum")} {item.totalPrice} {t("currency")}
                                                            </Typography>
                                                        </Box>
                                                    </AccordionSummary>
                                                    <AccordionDetails>
                                                        <Typography variant="h3" color="inherit" sx={{ mt: "15px" }}>
                                                            {t("pages.ordering.sellerProducts")} {item.shopName}
                                                        </Typography>
                                                        {item.basketItems.map((basketItem, index) => {

                                                            return (
                                                                <Box key={`order_basket_item_${basketItem.id}`} sx={{ display: "flex", justifyContent: "space-between", alignItems: "center", py: "15px", pr: "30px" }}>
                                                                    <Box sx={{ display: "flex", alignItems: "center" }}>
                                                                        <img
                                                                            style={{ width: "120px", height: "120px", objectFit: "contain", marginRight: "10px" }}
                                                                            src={basketItem.productImage != "" ? basketItem.productImage : small_empty}
                                                                            alt="productImage"
                                                                        />
                                                                        <LinkRouter underline="hover" color="inherit" to={`/product/${basketItem.productUrlSlug}`}>
                                                                            <Typography variant="h4" color="inherit" sx={{ width: "450px" }}>
                                                                                {basketItem.productName}
                                                                            </Typography>
                                                                        </LinkRouter>
                                                                    </Box>
                                                                    <Box sx={{ display: "flex", flexDirection: "column" }}>
                                                                        <Typography variant="h4" color="inherit" align="center">
                                                                            {t("pages.ordering.price")}
                                                                        </Typography>
                                                                        <Typography variant="h4" color="inherit" align="center">
                                                                            {basketItem.productDiscount} {t("currency")}
                                                                        </Typography>
                                                                    </Box>
                                                                    <Box sx={{ display: "flex", flexDirection: "column" }}>
                                                                        <Typography variant="h4" color="inherit" align="center">
                                                                            {t("pages.ordering.count")}
                                                                        </Typography>
                                                                        <Typography variant="h4" color="inherit" align="center">
                                                                            {basketItem.count}
                                                                        </Typography>
                                                                    </Box>
                                                                    <Box sx={{ display: "flex", flexDirection: "column" }}>
                                                                        <Typography variant="h4" color="inherit" align="center">
                                                                            {t("pages.ordering.sum")}
                                                                        </Typography>
                                                                        <Typography variant="h4" color="inherit" align="center">
                                                                            {basketItem.productPriceSum} {t("currency")}
                                                                        </Typography>
                                                                    </Box>
                                                                </Box>
                                                            )
                                                        })}
                                                    </AccordionDetails>
                                                </Accordion>
                                                : (index + 1 > currentOrder
                                                    && <Accordion
                                                        key={`order_${index}`}
                                                        expanded={expanded == index}
                                                        onChange={handleChange(index)}
                                                        sx={{
                                                            boxShadow: 0,
                                                            "&:before": {
                                                                background: "inherit"
                                                            }
                                                        }}
                                                    >
                                                        <AccordionSummary expandIcon={<ExpandMore />} sx={{ borderTop: "1px solid #7e7e7e" }}>
                                                            <Box
                                                                sx={{
                                                                    display: "flex",
                                                                    alignItems: "end"
                                                                }}
                                                            >
                                                                <Typography variant="h2" color="#7e7e7e" sx={{ mr: "180px" }}>
                                                                    {t("pages.ordering.order")} №{index + 1}
                                                                </Typography>
                                                                <Typography variant="h4" color="#7e7e7e">
                                                                    {t("pages.ordering.forTheSum")} {item.totalPrice} {t("currency")}
                                                                </Typography>
                                                            </Box>
                                                        </AccordionSummary>
                                                        <AccordionDetails>
                                                            <Typography variant="h3" color="#7e7e7e" sx={{ mt: "15px" }}>
                                                                {t("pages.ordering.sellerProducts")} {item.shopName}
                                                            </Typography>
                                                            {item.basketItems.map((basketItem, index) => {

                                                                return (
                                                                    <Box key={`order_basket_item_${basketItem.id}`} sx={{ display: "flex", justifyContent: "space-between", alignItems: "center", py: "15px" }}>
                                                                        <Box sx={{ display: "flex", alignItems: "center" }}>
                                                                            <img
                                                                                style={{ width: "120px", height: "120px", objectFit: "contain", marginRight: "10px" }}
                                                                                src={basketItem.productImage != "" ? basketItem.productImage : small_empty}
                                                                                alt="productImage"
                                                                            />
                                                                            <LinkRouter underline="hover" color="inherit" to={`/product/${basketItem.productUrlSlug}`}>
                                                                                <Typography variant="h4" color="inherit" sx={{ width: "550px" }}>
                                                                                    {basketItem.productName}
                                                                                </Typography>
                                                                            </LinkRouter>
                                                                        </Box>
                                                                        <Box sx={{ display: "flex", flexDirection: "column" }}>
                                                                            <Typography variant="h4" color="inherit" align="center">
                                                                                {t("pages.ordering.price")}
                                                                            </Typography>
                                                                            <Typography variant="h4" color="inherit" align="center">
                                                                                {basketItem.productDiscount} {t("currency")}
                                                                            </Typography>
                                                                        </Box>
                                                                        <Box sx={{ display: "flex", flexDirection: "column" }}>
                                                                            <Typography variant="h4" color="inherit" align="center">
                                                                                {t("pages.ordering.count")}
                                                                            </Typography>
                                                                            <Typography variant="h4" color="inherit" align="center">
                                                                                {basketItem.count}
                                                                            </Typography>
                                                                        </Box>
                                                                        <Box sx={{ display: "flex", flexDirection: "column" }}>
                                                                            <Typography variant="h4" color="inherit" align="center">
                                                                                {t("pages.ordering.sum")}
                                                                            </Typography>
                                                                            <Typography variant="h4" color="inherit" align="center">
                                                                                {basketItem.productPriceSum} {t("currency")}
                                                                            </Typography>
                                                                        </Box>
                                                                    </Box>
                                                                )
                                                            })}
                                                        </AccordionDetails>
                                                    </Accordion>)
                                            }
                                        </>
                                    )
                                })}
                                <Grid container sx={{ mt: "20px" }} spacing="30px">
                                    <Grid item xs={12}>
                                        <Typography variant="h2" color="inherit">
                                            {t("validationProps.deliveryType")}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={4}>
                                        <TextFieldSecondStyle
                                            select
                                            fullWidth
                                            variant="outlined"
                                            label={t("validationProps.deliveryType")}
                                            error={Boolean(touched.deliveryTypeId && errors.deliveryTypeId)}
                                            helperText={touched.deliveryTypeId && errors.deliveryTypeId}
                                            {...getFieldProps('deliveryTypeId')}
                                        >
                                            {orderProducts[currentOrder - 1]?.deliveryTypes && orderProducts[currentOrder - 1].deliveryTypes.map((item) =>
                                                <MenuItem key={item.id} value={item.id}>{item.name}</MenuItem>
                                            )}
                                        </TextFieldSecondStyle>
                                    </Grid>
                                    <Grid item xs={4}>
                                        <TextFieldSecondStyle
                                            fullWidth
                                            variant="outlined"
                                            type="text"
                                            placeholder={t("validationProps.city")}
                                            {...getFieldProps('city')}
                                            error={Boolean(touched.city && errors.city)}
                                            helperText={touched.city && errors.city}
                                        />
                                    </Grid>
                                    <Grid item xs={4}>
                                        <TextFieldSecondStyle
                                            fullWidth
                                            variant="outlined"
                                            type="text"
                                            placeholder={t("validationProps.department")}
                                            {...getFieldProps('department')}
                                            error={Boolean(touched.department && errors.department)}
                                            helperText={touched.department && errors.department}
                                        />
                                    </Grid>
                                </Grid>
                                <Box sx={{ mt: "40px", mb: "60px" }}>
                                    <Typography variant="h2" color="inherit" sx={{ mb: "20px" }}>
                                        {t("pages.ordering.paymentType")}
                                    </Typography>
                                    <FormControlLabel control={<Checkbox color="primary" checked={true} sx={{ borderRadius: "6px" }} defaultChecked />} label={<Typography variant="h3" color="inherit">
                                        {t("pages.ordering.paymentUponReceipt")}
                                    </Typography>} />
                                </Box>
                            </Grid>
                        </Grid>
                        <Grid item xs={1} />
                        <Grid item xs={3} >
                            <Box sx={{ position: "sticky", top: "20px" }}>
                                <Typography variant="h2" color="inherit">
                                    {t("pages.ordering.inGeneral")}
                                </Typography>
                                <Box sx={{ display: "flex", justifyContent: "space-between", mt: "35px" }}>
                                    <Typography variant="h4" color="inherit" fontWeight={700}>
                                        {orderProducts[currentOrder - 1]?.totalCount} {orderProducts[currentOrder - 1]?.totalCount == 1 ? t("pages.ordering.countProductsOne") : (lastCharOfCountProducts(orderProducts[currentOrder - 1]?.totalCount) ? t("pages.ordering.countProductsLessFive") : t("pages.ordering.countProducts"))} {t("pages.ordering.forTheSum")}:
                                    </Typography>
                                    <Typography variant="h4" color="inherit">
                                        {orderProducts[currentOrder - 1]?.totalPrice} {t("currency")}
                                    </Typography>
                                </Box>
                                <Box sx={{ display: "flex", justifyContent: "space-between", mt: "20px" }}>
                                    <Typography variant="h4" color="inherit" fontWeight={700} sx={{ height: "25px", my: "auto" }}>
                                        {t("pages.ordering.shippingCost")}:
                                    </Typography>
                                    <Typography variant="h4" color="inherit" align="right" sx={{ width: "200px" }}>
                                        {t("pages.ordering.accordingCarrierTariffs")}
                                    </Typography>
                                </Box>
                                <Box sx={{ display: "flex", justifyContent: "space-between", mt: "20px" }}>
                                    <Typography variant="h4" color="inherit" fontWeight={700}>
                                        {t("pages.ordering.amountPayable")}:
                                    </Typography>
                                    <Typography variant="h4" color="inherit">
                                        {orderProducts[currentOrder - 1]?.totalPrice} {t("currency")}
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
