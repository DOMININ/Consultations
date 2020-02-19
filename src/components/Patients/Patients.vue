<template>
  <div>
    <ul>
      <li
        v-for="opportunity in opportunities"
        v-bind:key="opportunity.component"
        v-on:click="currentOpportunities = opportunity.component"
      >
        {{ opportunity.tab }}
      </li>
    </ul>
    <div>
      <div
        v-if="currentOpportunities"
      >
        <component v-bind:is="selectedOpportunityComponent" />
      </div>
      <div v-else>
        Выберите нужную Вам вкладку
      </div>
    </div>
  </div>
</template>

<script>
  import AllPatients from "./AllPatients";
  import CreatePatients from "./CreatePatients";

  export default {
    name: "Patient",
    components: {AllPatients, CreatePatients},
    data() {
      return {
        currentOpportunities: null,
        opportunities: [
          {
            component: 'AllPatients',
            tab: 'Все пациенты'
          },
          {
            component: 'CreatePatients',
            tab: 'Создание пациента'
          },
          {
            component: 'EditPatients',
            tab:'Редактирование пациента'
          },
          {
            component: 'ConsultPatients',
            tab: 'Консультации пациента'
          },
          {
            component: 'DeletePatients',
            tab: 'Удаление пациента'
          }
        ]
      }
    },
    computed: {
      selectedOpportunityComponent: function() {
        return this.currentOpportunities;
      }
    },
  }
</script>

<style scoped>

</style>