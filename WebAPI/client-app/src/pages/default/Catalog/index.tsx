import { Box, Typography } from '@mui/material';
import { useEffect } from 'react'
import { useTranslation } from 'react-i18next';

import BreadcrumbsComponent from '../../../components/BreadcrumbsComponent';
import CatalogItem from '../../../components/CatalogItem';

import { useActions } from '../../../hooks/useActions';
import { useTypedSelector } from '../../../hooks/useTypedSelector';
import { BoxCatalogStyle } from './styled';

const Catalog = () => {
    const { t } = useTranslation();

    const { GetCatalog } = useActions();
    const { name, catalogItems } = useTypedSelector(state => state.catalog);

    useEffect(() => {
        document.title = `${t("components.breadcrumbs.catalog")}`;
        getData();
    }, [])

    const getData = async () => {
        try {
            await GetCatalog();
        } catch (ex) {
        }
    };

    return (
        <>
            <BreadcrumbsComponent parents={[]} />
            <Typography variant='h1' color="inherit" sx={{ marginBottom: "30px" }}>
                {t("components.breadcrumbs.catalog")}
            </Typography>
            <BoxCatalogStyle>
                {catalogItems.map((row, index) => {
                    return (
                        <CatalogItem key={index} name={row.name} image={row.image} urlSlug={row.urlSlug} />
                    );
                })}
            </BoxCatalogStyle>
        </>
    );
}

export default Catalog