import Typography from "@mui/material/Typography";
import Box from "@mui/material/Box";

import { useTranslation } from 'react-i18next';

import { outline_shop, shopping_bag_85 } from "../../../../assets/icons";

const ContactInfo = () => {
    const { t } = useTranslation();

    return (
        <Box sx={{
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
            justifyContent: "space-between",
        }}
        >
            <Box sx={{ display: "flex", justifyContent: "space-between", width: "1188px" }}>
                <Box sx={{ width: "480px" }}>
                    <Box sx={{ display: "flex", alignItems: "center", }}>
                        <img src={outline_shop} alt="icon outline shop" />
                        <Typography variant="h3" sx={{ fontWeight: "600", ml: "30px" }}>{t('pages.contactInfo.sellers.title')}</Typography>
                    </Box>
                    <Typography variant="h5" sx={{ fontWeight: "500", mt: "35px" }}>
                        {t('pages.contactInfo.sellers.description')}
                        <Typography sx={{ fontWeight: "700" }}>{t('pages.contactInfo.sellers.date')}</Typography>
                        <Typography>{t('pages.contactInfo.sellers.work')}</Typography>
                        <Typography>{t('pages.contactInfo.sellers.weekend')}</Typography>
                    </Typography>
                </Box>
                <Box sx={{ width: "480px" }}>
                    <Box sx={{ display: "flex", alignItems: "center", }}>
                        <img src={shopping_bag_85} alt="icon shopping bag" />
                        <Typography variant="h3" sx={{ fontWeight: "600", ml: "30px" }}>{t('pages.contactInfo.buyers.title')}</Typography>
                    </Box>
                    <Typography variant="h5" sx={{ mt: "35px" }}>
                        {t('pages.contactInfo.buyers.description')}
                    </Typography>
                </Box>
            </Box>
            <Box sx={{ width: "1188px", mt: "100px" }}>
                <Typography variant="h5" sx={{ fontWeight: "500" }}>
                    {t('pages.contactInfo.description')}
                    <Typography variant="h5" sx={{ fontWeight: "700" }}>+380 (73) 678 32 40</Typography>
                    <Typography variant="h5">E-mail: <Typography variant="h5" sx={{ display: "inline", fontWeight: "700" }}>info@comfy.ua</Typography></Typography>
                </Typography>
            </Box>
        </Box>
    );
}

export default ContactInfo;