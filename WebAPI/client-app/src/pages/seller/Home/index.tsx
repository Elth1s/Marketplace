import Tabs from '@mui/material/Tabs';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';

import { useState } from "react";
import { useTranslation } from 'react-i18next';

import About from "./Tab/About";
import Reviews from "./Tab/Reviews";

import { TabStyle } from './styled';
import AddReview from './AddReview';

interface TabPanelProps {
    children?: React.ReactNode;
    index: number;
    value: number;
}

function TabPanel(props: TabPanelProps) {
    const { children, value, index, ...other } = props;

    return (
        <div
            role="tabpanel"
            hidden={value !== index}
            id={`simple-tabpanel-${index}`}
            aria-labelledby={`simple-tab-${index}`}
            {...other}
        >
            {value === index && (
                <Box sx={{ mt: "50px" }}>
                    {children}
                </Box>
            )}
        </div>
    );
}

function a11yProps(index: number) {
    return {
        id: `simple-tab-${index}`,
        'aria-controls': `simple-tabpanel-${index}`,
    };
}

const SellerInfo = () => {
    const { t } = useTranslation();
    const [value, setValue] = useState(0);

    const handleChange = (event: React.SyntheticEvent, newValue: number) => {
        setValue(newValue);
    };

    return (
        <>
            <Typography variant='h1' sx={{ mb: "30px" }}>{t('pages.seller.title')}</Typography>

            <Grid container sx={{ justifyContent: "space-between" }}>
                <Grid item>
                    <Tabs value={value} onChange={handleChange} sx={{ minHeight: "0" }} >
                        <TabStyle label={t('pages.seller.tabs.about')} {...a11yProps(0)} />
                        <TabStyle label={t('pages.seller.tabs.product')} {...a11yProps(1)} />
                        <TabStyle label={t('pages.seller.tabs.reviews')} {...a11yProps(2)} />
                    </Tabs>
                </Grid>
                <Grid item>
                    {value === 2 ? (<AddReview />) : (<></>)}
                </Grid>
            </Grid>

            <TabPanel value={value} index={0}>
                <About />
            </TabPanel>
            <TabPanel value={value} index={1}>
                {/* <About /> */}
            </TabPanel>
            <TabPanel value={value} index={2}>
                <Reviews />
            </TabPanel>
        </>
    );
}

export default SellerInfo;