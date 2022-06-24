import Box from "@mui/material/Box"
import Tab from "@mui/material/Tab"
import Typography from "@mui/material/Typography"
import Grid from "@mui/material/Grid"

import TabContext from "@mui/lab/TabContext";
import TabList from "@mui/lab/TabList";
import TabPanel from "@mui/lab/TabPanel";

import React, { useState } from "react";

import ProductMainPage from "./ProductMainPage";
import ProductReviewsPage from "./ProductReviewsPage";
import ProductCharacteristicsPage from "./ProductCharacteristicsPage";

import CardProduct from "../../../components/CardProduct";
import { product, dataTabs } from "./data";

const Product = () => {
    const [valueTab, setValueTab] = useState<string>("0");

    const handleChange = (event: React.SyntheticEvent, newValue: string) => {
        setValueTab(newValue);
    };

    return (
        <>
            <TabContext value={valueTab} >
                <Box>
                    <TabList onChange={handleChange} aria-label="basic tabs example" >
                        {dataTabs.map((item, index) => (
                            <Tab key={index} label={item.label} value={index.toString()} />
                        ))}
                    </TabList >
                </Box>

                <Box sx={{ mb: "50px" }}>
                    <Typography variant="h4" sx={{ mt: "30px", mb: "15px" }}>Намисто з коштовностями</Typography>
                    <Typography variant="h6">Продавець GHOP Рейтинг</Typography>
                </Box>

                <TabPanel sx={{p:"0px"}} value="0" >
                    <ProductMainPage />
                </TabPanel>
                <TabPanel sx={{p:"0px"}} value="1">
                    <ProductReviewsPage />
                </TabPanel>
                <TabPanel sx={{p:"0px"}} value="2">
                    <ProductCharacteristicsPage />
                </TabPanel>

            </TabContext>

            <Grid container>
                <Typography variant="h4" sx={{ mb: "40px" }}>Схожі товари</Typography>
                <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                    {product.map((item, index) => (
                        <CardProduct
                            key={index}
                            image={item.image}
                            title={item.title}
                            status={item.status}
                            price={item.price}
                        />
                    ))}
                </Box>
            </Grid>
        </>
    );
}

export default Product;