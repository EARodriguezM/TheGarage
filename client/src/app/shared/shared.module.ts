import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';

import {
  NbActionsModule,
  NbLayoutModule,
  NbMenuModule,
  NbSearchModule,
  NbSidebarModule,
  NbUserModule,
  NbContextMenuModule,
  NbButtonModule,
  NbSelectModule,
  NbIconModule,
} from '@nebular/theme';

import { NbEvaIconsModule } from '@nebular/eva-icons';
//import { NbSecurityModule } from '@nebular/security';

import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';
import { NbSharedModule } from '@nebular/theme/components/shared/shared.module';

const NB_MODULES = [
  NbLayoutModule,
  NbMenuModule,
  NbUserModule,
  NbActionsModule,
  NbSearchModule,
  NbSidebarModule,
  NbContextMenuModule,
  NbButtonModule,
  NbSelectModule,
  NbIconModule,
  NbEvaIconsModule,
];

const COMPONENTS = [
  FooterComponent,
  HeaderComponent
]

@NgModule({
  declarations: [
    ...COMPONENTS
  ],
  imports: [
    CommonModule,
    ...NB_MODULES,
  ],
  exports:[
    CommonModule,
    ...COMPONENTS
  ]
})

export class SharedModule {}
