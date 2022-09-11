import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Grid from "@mui/material/Grid";
import Rating from "@mui/material/Rating";
import Typography from "@mui/material/Typography";
import { useEffect } from "react";
import { useTranslation } from "react-i18next";
import { useActions } from "../../../hooks/useActions";
import { useTypedSelector } from "../../../hooks/useTypedSelector";

import ProductItem from "../../../components/ProductItem/small";
import { BoxStyled } from "../styled";

const Reviewed = () => {
    const { t } = useTranslation();

    const { GetReviewedProducts } = useActions();
    const { userProducts } = useTypedSelector(state => state.catalog);

    useEffect(() => {
        document.title = `${t("pages.user.menu.reviewedProducts")}`;
        getData();
    }, [])

    const getData = async () => {
        try {
            await GetReviewedProducts();
        } catch (ex) {
        }
    };

    return (
        <>
            <Typography variant="h1" color="inherit" sx={{ mb: "25px" }}>
                {t("pages.user.menu.reviewedProducts")}
            </Typography>
            <BoxStyled>
                {userProducts != null && userProducts.map((row, index) => {
                    return (
                        <ProductItem key={row.urlSlug} isInCart={row.isInBasket} isSelected={row.isSelected} discount={row.discount} name={row.name} image={row.image} statusName={row.statusName} price={row.price} urlSlug={row.urlSlug} />
                    );
                })}
            </BoxStyled>
        </>
    );
}

export default Reviewed;