import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { EditarUnidadMedidaPage } from './editar-unidad-medida.page';

describe('EditarUnidadMedidaPage', () => {
  let component: EditarUnidadMedidaPage;
  let fixture: ComponentFixture<EditarUnidadMedidaPage>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ EditarUnidadMedidaPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(EditarUnidadMedidaPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
