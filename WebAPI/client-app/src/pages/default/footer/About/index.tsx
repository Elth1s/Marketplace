import { Breadcrumbs, Typography, Grid, useTheme } from "@mui/material";
import { NavigateNext } from "@mui/icons-material"
import { useTranslation } from "react-i18next";

import { about } from "../../../../assets/backgrounds";
import LinkRouter from "../../../../components/LinkRouter";

const About = () => {
    const { t } = useTranslation();
    const { palette } = useTheme();

    return (
        <Grid container>
            <Grid item xs={12}>
                <Breadcrumbs aria-label="breadcrumb" color="inherit" sx={{ marginBottom: "50px" }} separator={<NavigateNext sx={{ color: "#7e7e7e" }} fontSize="small" />} >
                    <LinkRouter underline="none" color="inherit" to="/">
                        {t("components.breadcrumbs.home")}
                    </LinkRouter>
                    <Typography color="#7e7e7e">
                        {t("containers.default.footer.about.title")}
                    </Typography>
                </Breadcrumbs>
            </Grid>
            <Grid item xs={7}>
                <Typography variant="h1" color="inherit">{t('pages.about.aboutUs.title')}</Typography>
                <Typography variant="h2" color="inherit" sx={{ mt: "25px" }}>{t('pages.about.aboutUs.desc')}</Typography>
                <Typography variant="h1" color="inherit" sx={{ mt: "30px" }}>{t('pages.about.ourGoal.title')}</Typography>
                <Typography variant="h2" color="inherit" sx={{ mt: "25px" }}>{t('pages.about.ourGoal.desc')}</Typography>
                <Typography variant="h1" color="inherit" sx={{ mt: "30px" }}>{t('pages.about.ourTeam.title')}</Typography>
            </Grid>
            <Grid item xs={5} sx={{ background: palette.background.default }}>
                <img src={about} alt="image" style={{ width: "100%", height: "100%" }} />
            </Grid>
        </Grid>
    );
}

export default About;