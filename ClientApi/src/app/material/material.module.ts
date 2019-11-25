import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTabsModule, MatSidenavModule, MatToolbarModule, MatIconModule, MatButtonModule,MatFormFieldModule, MatListModule,MatDatepickerModule,
   MatNativeDateModule, MatInputModule, MatSortModule, MatSelectModule, MatCardModule, MatCheckboxModule, MatProgressBarModule, MatTableModule, MatTableDataSource } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';

@NgModule({
  declarations: [
  ],
  imports: [
    MatTableModule,
    CommonModule,
    MatTabsModule,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatListModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatProgressBarModule,
    MatInputModule,
    MatSortModule,
    MatSelectModule,
    MatCardModule,
    MatCheckboxModule,
    FlexLayoutModule
  ],
  exports: [
    MatTableModule,
    MatProgressBarModule,
    MatTabsModule,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatListModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatInputModule,
    MatSortModule,
    MatSelectModule,
    MatCardModule,
    MatCheckboxModule,
    FlexLayoutModule
  ]
})
export class MaterialModule { }
